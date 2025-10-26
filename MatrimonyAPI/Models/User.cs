using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MatrimonyAPI.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;
    
    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [BsonElement("lastLoginAt")]
    public DateTime? LastLoginAt { get; set; }
    
    [BsonElement("isActive")]
    public bool IsActive { get; set; } = true;
    
    [BsonElement("isEmailVerified")]
    public bool IsEmailVerified { get; set; } = false;
    
    [BsonElement("isPhoneVerified")]
    public bool IsPhoneVerified { get; set; } = false;
    
    [BsonElement("role")]
    [BsonRepresentation(BsonType.String)]
    public UserRole Role { get; set; } = UserRole.User;
}

public enum UserRole
{
    User,
    Admin,
    Moderator
}

