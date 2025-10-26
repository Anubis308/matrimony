using MongoDB.Driver;
using MatrimonyAPI.Data;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Models;

namespace MatrimonyAPI.Services;

public class ProfileService : IProfileService
{
    private readonly MongoDbContext _context;

    public ProfileService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<ProfileResponse?> CreateProfileAsync(string userId, CreateProfileRequest request)
    {
        var user = await _context.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null)
        {
            return null;
        }

        var existingProfile = await _context.Profiles.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        if (existingProfile != null)
        {
            return null;
        }

        var profile = new Profile
        {
            UserId = userId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Religion = request.Religion,
            Community = request.Community,
            MotherTongue = request.MotherTongue,
            MaritalStatus = request.MaritalStatus,
            HeightInCm = request.HeightInCm,
            Education = request.Education,
            Occupation = request.Occupation,
            AnnualIncome = request.AnnualIncome,
            Country = request.Country,
            State = request.State,
            City = request.City,
            About = request.About,
            FamilyDetails = request.FamilyDetails,
            Hobbies = request.Hobbies,
            IsProfileComplete = true,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Profiles.InsertOneAsync(profile);
        return await GetProfileAsync(profile.Id);
    }

    public async Task<ProfileResponse?> GetProfileAsync(string profileId)
    {
        var profile = await _context.Profiles.Find(p => p.Id == profileId).FirstOrDefaultAsync();
        if (profile == null)
        {
            return null;
        }

        var photos = await _context.Photos.Find(p => p.UserId == profile.UserId).ToListAsync();
        return MapToProfileResponse(profile, photos);
    }

    public async Task<ProfileResponse?> GetUserProfileAsync(string userId)
    {
        var profile = await _context.Profiles.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        if (profile == null)
        {
            return null;
        }

        var photos = await _context.Photos.Find(p => p.UserId == userId).ToListAsync();
        return MapToProfileResponse(profile, photos);
    }

    public async Task<ProfileResponse?> UpdateProfileAsync(string userId, UpdateProfileRequest request)
    {
        var profile = await _context.Profiles.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        if (profile == null)
        {
            return null;
        }

        var updateBuilder = Builders<Profile>.Update;
        var updates = new List<UpdateDefinition<Profile>>();

        if (request.FirstName != null) updates.Add(updateBuilder.Set(p => p.FirstName, request.FirstName));
        if (request.LastName != null) updates.Add(updateBuilder.Set(p => p.LastName, request.LastName));
        if (request.DateOfBirth != null) updates.Add(updateBuilder.Set(p => p.DateOfBirth, request.DateOfBirth.Value));
        if (request.Religion != null) updates.Add(updateBuilder.Set(p => p.Religion, request.Religion));
        if (request.Community != null) updates.Add(updateBuilder.Set(p => p.Community, request.Community));
        if (request.MotherTongue != null) updates.Add(updateBuilder.Set(p => p.MotherTongue, request.MotherTongue));
        if (request.MaritalStatus != null) updates.Add(updateBuilder.Set(p => p.MaritalStatus, request.MaritalStatus.Value));
        if (request.HeightInCm != null) updates.Add(updateBuilder.Set(p => p.HeightInCm, request.HeightInCm.Value));
        if (request.Education != null) updates.Add(updateBuilder.Set(p => p.Education, request.Education));
        if (request.Occupation != null) updates.Add(updateBuilder.Set(p => p.Occupation, request.Occupation));
        if (request.AnnualIncome != null) updates.Add(updateBuilder.Set(p => p.AnnualIncome, request.AnnualIncome));
        if (request.Country != null) updates.Add(updateBuilder.Set(p => p.Country, request.Country));
        if (request.State != null) updates.Add(updateBuilder.Set(p => p.State, request.State));
        if (request.City != null) updates.Add(updateBuilder.Set(p => p.City, request.City));
        if (request.About != null) updates.Add(updateBuilder.Set(p => p.About, request.About));
        if (request.FamilyDetails != null) updates.Add(updateBuilder.Set(p => p.FamilyDetails, request.FamilyDetails));
        if (request.Hobbies != null) updates.Add(updateBuilder.Set(p => p.Hobbies, request.Hobbies));
        
        updates.Add(updateBuilder.Set(p => p.UpdatedAt, DateTime.UtcNow));

        if (updates.Any())
        {
            var combinedUpdate = updateBuilder.Combine(updates);
            await _context.Profiles.UpdateOneAsync(p => p.Id == profile.Id, combinedUpdate);
        }

        return await GetProfileAsync(profile.Id);
    }

    public async Task<bool> DeleteProfileAsync(string userId)
    {
        var result = await _context.Profiles.DeleteOneAsync(p => p.UserId == userId);
        return result.DeletedCount > 0;
    }

    public async Task<PhotoResponse?> UploadPhotoAsync(string userId, UploadPhotoRequest request)
    {
        var user = await _context.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null)
        {
            return null;
        }

        // If this is set as primary, unset other primary photos
        if (request.IsPrimary)
        {
            var update = Builders<Photo>.Update.Set(p => p.IsPrimary, false);
            await _context.Photos.UpdateManyAsync(p => p.UserId == userId && p.IsPrimary, update);
        }

        var newPhoto = new Photo
        {
            UserId = userId,
            Url = request.Url,
            IsPrimary = request.IsPrimary,
            IsApproved = false,
            UploadedAt = DateTime.UtcNow
        };

        await _context.Photos.InsertOneAsync(newPhoto);

        return new PhotoResponse
        {
            Id = newPhoto.Id,
            Url = newPhoto.Url,
            IsPrimary = newPhoto.IsPrimary,
            IsApproved = newPhoto.IsApproved
        };
    }

    public async Task<bool> DeletePhotoAsync(string userId, string photoId)
    {
        var result = await _context.Photos.DeleteOneAsync(p => p.Id == photoId && p.UserId == userId);
        return result.DeletedCount > 0;
    }

    public async Task<bool> SetPrimaryPhotoAsync(string userId, string photoId)
    {
        var photo = await _context.Photos.Find(p => p.Id == photoId && p.UserId == userId).FirstOrDefaultAsync();
        if (photo == null)
        {
            return false;
        }

        // Unset all primary photos for this user
        var unsetUpdate = Builders<Photo>.Update.Set(p => p.IsPrimary, false);
        await _context.Photos.UpdateManyAsync(p => p.UserId == userId, unsetUpdate);

        // Set this photo as primary
        var setPrimaryUpdate = Builders<Photo>.Update.Set(p => p.IsPrimary, true);
        await _context.Photos.UpdateOneAsync(p => p.Id == photoId, setPrimaryUpdate);

        return true;
    }

    private ProfileResponse MapToProfileResponse(Profile profile, List<Photo> photos)
    {
        var age = DateTime.UtcNow.Year - profile.DateOfBirth.Year;
        if (DateTime.UtcNow < profile.DateOfBirth.AddYears(age))
        {
            age--;
        }

        return new ProfileResponse
        {
            Id = profile.Id,
            UserId = profile.UserId,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Age = age,
            Gender = profile.Gender,
            Religion = profile.Religion,
            Community = profile.Community,
            MotherTongue = profile.MotherTongue,
            MaritalStatus = profile.MaritalStatus,
            HeightInCm = profile.HeightInCm,
            Education = profile.Education,
            Occupation = profile.Occupation,
            AnnualIncome = profile.AnnualIncome,
            Country = profile.Country,
            State = profile.State,
            City = profile.City,
            About = profile.About,
            FamilyDetails = profile.FamilyDetails,
            Hobbies = profile.Hobbies,
            Photos = photos.Select(p => new PhotoResponse
            {
                Id = p.Id,
                Url = p.Url,
                IsPrimary = p.IsPrimary,
                IsApproved = p.IsApproved
            }).ToList(),
            IsProfileComplete = profile.IsProfileComplete
        };
    }
}
