using MatrimonyAPI.DTOs;

namespace MatrimonyAPI.Services;

public interface IMessageService
{
    Task<MessageResponse?> SendMessageAsync(string senderId, SendMessageRequest request);
    Task<List<MessageResponse>> GetConversationAsync(string userId, string otherUserId);
    Task<List<ConversationResponse>> GetConversationsAsync(string userId);
    Task<bool> MarkAsReadAsync(string userId, string messageId);
    Task<int> GetUnreadCountAsync(string userId);
}

