using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MatrimonyAPI.Models;

public class Interest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("senderId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SenderId { get; set; } = string.Empty;
    
    [BsonElement("receiverId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ReceiverId { get; set; } = string.Empty;
    
    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public InterestStatus Status { get; set; } = InterestStatus.Pending;
    
    [BsonElement("message")]
    public string? Message { get; set; }
    
    [BsonElement("sentAt")]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    
    [BsonElement("respondedAt")]
    public DateTime? RespondedAt { get; set; }
}

public enum InterestStatus
{
    Pending,
    Accepted,
    Rejected,
    Cancelled
}

