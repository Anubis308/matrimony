using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MatrimonyAPI.Models;

public class Message
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
    
    [BsonElement("content")]
    public string Content { get; set; } = string.Empty;
    
    [BsonElement("isRead")]
    public bool IsRead { get; set; } = false;
    
    [BsonElement("sentAt")]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    
    [BsonElement("readAt")]
    public DateTime? ReadAt { get; set; }
}

