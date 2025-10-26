using MongoDB.Driver;
using MatrimonyAPI.Data;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Models;

namespace MatrimonyAPI.Services;

public class MessageService : IMessageService
{
    private readonly MongoDbContext _context;

    public MessageService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<MessageResponse?> SendMessageAsync(string senderId, SendMessageRequest request)
    {
        // Check if receiver exists and is active
        var receiver = await _context.Users.Find(u => u.Id == request.ReceiverId).FirstOrDefaultAsync();
        if (receiver == null || !receiver.IsActive)
        {
            return null;
        }

        // Check if there's an accepted interest between users
        var filterBuilder = Builders<Interest>.Filter;
        var connectionFilter = filterBuilder.And(
            filterBuilder.Or(
                filterBuilder.And(
                    filterBuilder.Eq(i => i.SenderId, senderId),
                    filterBuilder.Eq(i => i.ReceiverId, request.ReceiverId)
                ),
                filterBuilder.And(
                    filterBuilder.Eq(i => i.SenderId, request.ReceiverId),
                    filterBuilder.Eq(i => i.ReceiverId, senderId)
                )
            ),
            filterBuilder.Eq(i => i.Status, InterestStatus.Accepted)
        );

        var hasConnection = await _context.Interests.Find(connectionFilter).AnyAsync();
        if (!hasConnection)
        {
            return null;
        }

        var message = new Message
        {
            SenderId = senderId,
            ReceiverId = request.ReceiverId,
            Content = request.Content,
            IsRead = false,
            SentAt = DateTime.UtcNow
        };

        await _context.Messages.InsertOneAsync(message);
        return await GetMessageResponseAsync(message.Id);
    }

    public async Task<List<MessageResponse>> GetConversationAsync(string userId, string otherUserId)
    {
        var filterBuilder = Builders<Message>.Filter;
        var conversationFilter = filterBuilder.Or(
            filterBuilder.And(
                filterBuilder.Eq(m => m.SenderId, userId),
                filterBuilder.Eq(m => m.ReceiverId, otherUserId)
            ),
            filterBuilder.And(
                filterBuilder.Eq(m => m.SenderId, otherUserId),
                filterBuilder.Eq(m => m.ReceiverId, userId)
            )
        );

        var messages = await _context.Messages
            .Find(conversationFilter)
            .SortBy(m => m.SentAt)
            .ToListAsync();

        var responses = new List<MessageResponse>();
        foreach (var message in messages)
        {
            var response = await GetMessageResponseAsync(message.Id);
            if (response != null)
            {
                responses.Add(response);
            }
        }

        return responses;
    }

    public async Task<List<ConversationResponse>> GetConversationsAsync(string userId)
    {
        var filterBuilder = Builders<Message>.Filter;
        var userMessagesFilter = filterBuilder.Or(
            filterBuilder.Eq(m => m.SenderId, userId),
            filterBuilder.Eq(m => m.ReceiverId, userId)
        );

        var messages = await _context.Messages.Find(userMessagesFilter).ToListAsync();

        // Group messages by other user
        var conversations = messages
            .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
            .Select(g => new
            {
                OtherUserId = g.Key,
                LastMessage = g.OrderByDescending(m => m.SentAt).FirstOrDefault(),
                UnreadCount = g.Count(m => m.ReceiverId == userId && !m.IsRead)
            })
            .ToList();

        var result = new List<ConversationResponse>();

        foreach (var conv in conversations)
        {
            var otherUserProfile = await _context.Profiles.Find(p => p.UserId == conv.OtherUserId).FirstOrDefaultAsync();

            if (otherUserProfile != null)
            {
                result.Add(new ConversationResponse
                {
                    OtherUserId = conv.OtherUserId,
                    OtherUserName = $"{otherUserProfile.FirstName} {otherUserProfile.LastName}",
                    LastMessage = conv.LastMessage?.Content,
                    LastMessageTime = conv.LastMessage?.SentAt,
                    UnreadCount = conv.UnreadCount
                });
            }
        }

        return result.OrderByDescending(c => c.LastMessageTime).ToList();
    }

    public async Task<bool> MarkAsReadAsync(string userId, string messageId)
    {
        var message = await _context.Messages
            .Find(m => m.Id == messageId && m.ReceiverId == userId)
            .FirstOrDefaultAsync();

        if (message == null || message.IsRead)
        {
            return false;
        }

        var update = Builders<Message>.Update
            .Set(m => m.IsRead, true)
            .Set(m => m.ReadAt, DateTime.UtcNow);

        await _context.Messages.UpdateOneAsync(m => m.Id == messageId, update);
        return true;
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(m => m.ReceiverId, userId),
            Builders<Message>.Filter.Eq(m => m.IsRead, false)
        );

        var count = await _context.Messages.CountDocumentsAsync(filter);
        return (int)count;
    }

    private async Task<MessageResponse?> GetMessageResponseAsync(string messageId)
    {
        var message = await _context.Messages.Find(m => m.Id == messageId).FirstOrDefaultAsync();
        if (message == null)
        {
            return null;
        }

        var senderProfile = await _context.Profiles.Find(p => p.UserId == message.SenderId).FirstOrDefaultAsync();
        var receiverProfile = await _context.Profiles.Find(p => p.UserId == message.ReceiverId).FirstOrDefaultAsync();

        return new MessageResponse
        {
            Id = message.Id,
            SenderId = message.SenderId,
            SenderName = senderProfile != null
                ? $"{senderProfile.FirstName} {senderProfile.LastName}"
                : "Unknown",
            ReceiverId = message.ReceiverId,
            ReceiverName = receiverProfile != null
                ? $"{receiverProfile.FirstName} {receiverProfile.LastName}"
                : "Unknown",
            Content = message.Content,
            IsRead = message.IsRead,
            SentAt = message.SentAt,
            ReadAt = message.ReadAt
        };
    }
}
