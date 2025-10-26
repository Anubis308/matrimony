using MatrimonyAPI.Models;

namespace MatrimonyAPI.DTOs;

public class SendInterestRequest
{
    public string ReceiverId { get; set; } = string.Empty;
    public string? Message { get; set; }
}

public class RespondInterestRequest
{
    public string InterestId { get; set; } = string.Empty;
    public InterestStatus Status { get; set; } // Accepted or Rejected
}

public class InterestResponse
{
    public string Id { get; set; } = string.Empty;
    public string SenderId { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public InterestStatus Status { get; set; }
    public string? Message { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? RespondedAt { get; set; }
}

