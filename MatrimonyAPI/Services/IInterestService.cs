using MatrimonyAPI.DTOs;

namespace MatrimonyAPI.Services;

public interface IInterestService
{
    Task<InterestResponse?> SendInterestAsync(string senderId, SendInterestRequest request);
    Task<InterestResponse?> RespondToInterestAsync(string userId, RespondInterestRequest request);
    Task<List<InterestResponse>> GetSentInterestsAsync(string userId);
    Task<List<InterestResponse>> GetReceivedInterestsAsync(string userId);
    Task<bool> CancelInterestAsync(string userId, string interestId);
}

