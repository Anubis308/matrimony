using MongoDB.Driver;
using MatrimonyAPI.Data;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Models;

namespace MatrimonyAPI.Services;

public class InterestService : IInterestService
{
    private readonly MongoDbContext _context;

    public InterestService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<InterestResponse?> SendInterestAsync(string senderId, SendInterestRequest request)
    {
        // Check if receiver exists and is active
        var receiver = await _context.Users.Find(u => u.Id == request.ReceiverId).FirstOrDefaultAsync();
        if (receiver == null || !receiver.IsActive)
        {
            return null;
        }

        // Check if interest already exists
        var existingInterest = await _context.Interests
            .Find(i => i.SenderId == senderId && i.ReceiverId == request.ReceiverId)
            .FirstOrDefaultAsync();

        if (existingInterest != null)
        {
            return null;
        }

        var interest = new Interest
        {
            SenderId = senderId,
            ReceiverId = request.ReceiverId,
            Message = request.Message,
            Status = InterestStatus.Pending,
            SentAt = DateTime.UtcNow
        };

        await _context.Interests.InsertOneAsync(interest);
        return await GetInterestResponseAsync(interest.Id);
    }

    public async Task<InterestResponse?> RespondToInterestAsync(string userId, RespondInterestRequest request)
    {
        var interest = await _context.Interests
            .Find(i => i.Id == request.InterestId && i.ReceiverId == userId)
            .FirstOrDefaultAsync();

        if (interest == null || interest.Status != InterestStatus.Pending)
        {
            return null;
        }

        if (request.Status != InterestStatus.Accepted && request.Status != InterestStatus.Rejected)
        {
            return null;
        }

        var update = Builders<Interest>.Update
            .Set(i => i.Status, request.Status)
            .Set(i => i.RespondedAt, DateTime.UtcNow);

        await _context.Interests.UpdateOneAsync(i => i.Id == request.InterestId, update);
        return await GetInterestResponseAsync(request.InterestId);
    }

    public async Task<List<InterestResponse>> GetSentInterestsAsync(string userId)
    {
        var interests = await _context.Interests
            .Find(i => i.SenderId == userId)
            .SortByDescending(i => i.SentAt)
            .ToListAsync();

        var responses = new List<InterestResponse>();
        foreach (var interest in interests)
        {
            var response = await GetInterestResponseAsync(interest.Id);
            if (response != null)
            {
                responses.Add(response);
            }
        }

        return responses;
    }

    public async Task<List<InterestResponse>> GetReceivedInterestsAsync(string userId)
    {
        var interests = await _context.Interests
            .Find(i => i.ReceiverId == userId)
            .SortByDescending(i => i.SentAt)
            .ToListAsync();

        var responses = new List<InterestResponse>();
        foreach (var interest in interests)
        {
            var response = await GetInterestResponseAsync(interest.Id);
            if (response != null)
            {
                responses.Add(response);
            }
        }

        return responses;
    }

    public async Task<bool> CancelInterestAsync(string userId, string interestId)
    {
        var interest = await _context.Interests
            .Find(i => i.Id == interestId && i.SenderId == userId)
            .FirstOrDefaultAsync();

        if (interest == null || interest.Status != InterestStatus.Pending)
        {
            return false;
        }

        var update = Builders<Interest>.Update.Set(i => i.Status, InterestStatus.Cancelled);
        await _context.Interests.UpdateOneAsync(i => i.Id == interestId, update);

        return true;
    }

    private async Task<InterestResponse?> GetInterestResponseAsync(string interestId)
    {
        var interest = await _context.Interests.Find(i => i.Id == interestId).FirstOrDefaultAsync();
        if (interest == null)
        {
            return null;
        }

        var senderProfile = await _context.Profiles.Find(p => p.UserId == interest.SenderId).FirstOrDefaultAsync();
        var receiverProfile = await _context.Profiles.Find(p => p.UserId == interest.ReceiverId).FirstOrDefaultAsync();

        return new InterestResponse
        {
            Id = interest.Id,
            SenderId = interest.SenderId,
            SenderName = senderProfile != null
                ? $"{senderProfile.FirstName} {senderProfile.LastName}"
                : "Unknown",
            ReceiverId = interest.ReceiverId,
            ReceiverName = receiverProfile != null
                ? $"{receiverProfile.FirstName} {receiverProfile.LastName}"
                : "Unknown",
            Status = interest.Status,
            Message = interest.Message,
            SentAt = interest.SentAt,
            RespondedAt = interest.RespondedAt
        };
    }
}
