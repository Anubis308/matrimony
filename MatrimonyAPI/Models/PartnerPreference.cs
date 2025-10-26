using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MatrimonyAPI.Models;

public class PartnerPreference
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = string.Empty;
    
    [BsonElement("minAge")]
    public int? MinAge { get; set; }
    
    [BsonElement("maxAge")]
    public int? MaxAge { get; set; }
    
    [BsonElement("minHeightInCm")]
    public int? MinHeightInCm { get; set; }
    
    [BsonElement("maxHeightInCm")]
    public int? MaxHeightInCm { get; set; }
    
    [BsonElement("preferredReligions")]
    public string? PreferredReligions { get; set; }
    
    [BsonElement("preferredCommunities")]
    public string? PreferredCommunities { get; set; }
    
    [BsonElement("preferredMaritalStatus")]
    public string? PreferredMaritalStatus { get; set; }
    
    [BsonElement("preferredEducation")]
    public string? PreferredEducation { get; set; }
    
    [BsonElement("preferredOccupation")]
    public string? PreferredOccupation { get; set; }
    
    [BsonElement("minAnnualIncome")]
    public decimal? MinAnnualIncome { get; set; }
    
    [BsonElement("preferredCountries")]
    public string? PreferredCountries { get; set; }
    
    [BsonElement("preferredStates")]
    public string? PreferredStates { get; set; }
    
    [BsonElement("preferredCities")]
    public string? PreferredCities { get; set; }
}

