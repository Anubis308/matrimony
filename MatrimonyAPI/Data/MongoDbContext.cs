using MongoDB.Driver;
using MatrimonyAPI.Models;
using Microsoft.Extensions.Options;

namespace MatrimonyAPI.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        
        // Create indexes
        //CreateIndexes();
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<Profile> Profiles => _database.GetCollection<Profile>("profiles");
    public IMongoCollection<PartnerPreference> PartnerPreferences => _database.GetCollection<PartnerPreference>("partnerPreferences");
    public IMongoCollection<Photo> Photos => _database.GetCollection<Photo>("photos");
    public IMongoCollection<Interest> Interests => _database.GetCollection<Interest>("interests");
    public IMongoCollection<Message> Messages => _database.GetCollection<Message>("messages");

    private void CreateIndexes()
    {
        // Create unique index on User email
        var userEmailIndexKeys = Builders<User>.IndexKeys.Ascending(u => u.Email);
        var userEmailIndexOptions = new CreateIndexOptions { Unique = true };
        Users.Indexes.CreateOne(new CreateIndexModel<User>(userEmailIndexKeys, userEmailIndexOptions));

        // Create index on Profile userId
        var profileUserIdIndexKeys = Builders<Profile>.IndexKeys.Ascending(p => p.UserId);
        var profileUserIdIndexOptions = new CreateIndexOptions { Unique = true };
        Profiles.Indexes.CreateOne(new CreateIndexModel<Profile>(profileUserIdIndexKeys, profileUserIdIndexOptions));

        // Create indexes on Photo userId
        var photoUserIdIndexKeys = Builders<Photo>.IndexKeys.Ascending(p => p.UserId);
        Photos.Indexes.CreateOne(new CreateIndexModel<Photo>(photoUserIdIndexKeys));

        // Create indexes on Interest
        var interestSenderIndexKeys = Builders<Interest>.IndexKeys.Ascending(i => i.SenderId);
        Interests.Indexes.CreateOne(new CreateIndexModel<Interest>(interestSenderIndexKeys));
        
        var interestReceiverIndexKeys = Builders<Interest>.IndexKeys.Ascending(i => i.ReceiverId);
        Interests.Indexes.CreateOne(new CreateIndexModel<Interest>(interestReceiverIndexKeys));

        // Create indexes on Message
        var messageSenderIndexKeys = Builders<Message>.IndexKeys.Ascending(m => m.SenderId);
        Messages.Indexes.CreateOne(new CreateIndexModel<Message>(messageSenderIndexKeys));
        
        var messageReceiverIndexKeys = Builders<Message>.IndexKeys.Ascending(m => m.ReceiverId);
        Messages.Indexes.CreateOne(new CreateIndexModel<Message>(messageReceiverIndexKeys));

        // Create compound index for searching profiles
        var profileSearchIndexKeys = Builders<Profile>.IndexKeys
            .Ascending(p => p.Gender)
            .Ascending(p => p.Religion)
            .Ascending(p => p.City);
        Profiles.Indexes.CreateOne(new CreateIndexModel<Profile>(profileSearchIndexKeys));

        // Create index on PartnerPreference userId
        var preferenceUserIdIndexKeys = Builders<PartnerPreference>.IndexKeys.Ascending(p => p.UserId);
        var preferenceUserIdIndexOptions = new CreateIndexOptions { Unique = true };
        PartnerPreferences.Indexes.CreateOne(new CreateIndexModel<PartnerPreference>(preferenceUserIdIndexKeys, preferenceUserIdIndexOptions));
    }
}

