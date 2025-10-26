using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MatrimonyAPI.Models;

public class Profile
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = string.Empty;
    
    [BsonElement("firstName")]
    public string FirstName { get; set; } = string.Empty;
    
    [BsonElement("lastName")]
    public string LastName { get; set; } = string.Empty;
    
    [BsonElement("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }
    
    [BsonElement("gender")]
    [BsonRepresentation(BsonType.String)]
    public Gender Gender { get; set; }
    
    [BsonElement("religion")]
    public string Religion { get; set; } = string.Empty;
    
    [BsonElement("community")]
    public string Community { get; set; } = string.Empty;
    
    [BsonElement("motherTongue")]
    public string MotherTongue { get; set; } = string.Empty;
    
    [BsonElement("maritalStatus")]
    [BsonRepresentation(BsonType.String)]
    public MaritalStatus MaritalStatus { get; set; }
    
    [BsonElement("heightInCm")]
    public int HeightInCm { get; set; }
    
    [BsonElement("education")]
    public string Education { get; set; } = string.Empty;
    
    [BsonElement("occupation")]
    public string Occupation { get; set; } = string.Empty;
    
    [BsonElement("annualIncome")]
    public decimal? AnnualIncome { get; set; }
    
    [BsonElement("country")]
    public string Country { get; set; } = string.Empty;
    
    [BsonElement("state")]
    public string State { get; set; } = string.Empty;
    
    [BsonElement("city")]
    public string City { get; set; } = string.Empty;
    
    [BsonElement("about")]
    public string About { get; set; } = string.Empty;
    
    [BsonElement("familyDetails")]
    public string FamilyDetails { get; set; } = string.Empty;
    
    [BsonElement("hobbies")]
    public string Hobbies { get; set; } = string.Empty;
    
    [BsonElement("isProfileComplete")]
    public bool IsProfileComplete { get; set; } = false;
    
    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public enum Gender
{
    Male,
    Female,
    Other
}

public enum MaritalStatus
{
    NeverMarried,
    Divorced,
    Widowed,
    AwaitingDivorce
}

