using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MatrimonyAPI.Models;

public class Photo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = string.Empty;
    
    [BsonElement("url")]
    public string Url { get; set; } = string.Empty;
    
    [BsonElement("isPrimary")]
    public bool IsPrimary { get; set; } = false;
    
    [BsonElement("isApproved")]
    public bool IsApproved { get; set; } = false;
    
    [BsonElement("uploadedAt")]
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}

