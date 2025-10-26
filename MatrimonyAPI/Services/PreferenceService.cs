using MongoDB.Driver;
using MatrimonyAPI.Data;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Models;

namespace MatrimonyAPI.Services;

public class PreferenceService : IPreferenceService
{
    private readonly MongoDbContext _context;

    public PreferenceService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<PreferenceResponse?> CreateOrUpdatePreferenceAsync(string userId, PreferenceRequest request)
    {
        var profile = await _context.Profiles.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        if (profile == null)
        {
            return null;
        }

        var existingPreference = await _context.PartnerPreferences.Find(p => p.UserId == userId).FirstOrDefaultAsync();

        if (existingPreference == null)
        {
            // Create new preference
            var preference = new PartnerPreference
            {
                UserId = userId,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                MinHeightInCm = request.MinHeightInCm,
                MaxHeightInCm = request.MaxHeightInCm,
                PreferredReligions = request.PreferredReligions,
                PreferredCommunities = request.PreferredCommunities,
                PreferredMaritalStatus = request.PreferredMaritalStatus,
                PreferredEducation = request.PreferredEducation,
                PreferredOccupation = request.PreferredOccupation,
                MinAnnualIncome = request.MinAnnualIncome,
                PreferredCountries = request.PreferredCountries,
                PreferredStates = request.PreferredStates,
                PreferredCities = request.PreferredCities
            };

            await _context.PartnerPreferences.InsertOneAsync(preference);
            return MapToPreferenceResponse(preference);
        }
        else
        {
            // Update existing preference
            var updateBuilder = Builders<PartnerPreference>.Update;
            var update = updateBuilder
                .Set(p => p.MinAge, request.MinAge)
                .Set(p => p.MaxAge, request.MaxAge)
                .Set(p => p.MinHeightInCm, request.MinHeightInCm)
                .Set(p => p.MaxHeightInCm, request.MaxHeightInCm)
                .Set(p => p.PreferredReligions, request.PreferredReligions)
                .Set(p => p.PreferredCommunities, request.PreferredCommunities)
                .Set(p => p.PreferredMaritalStatus, request.PreferredMaritalStatus)
                .Set(p => p.PreferredEducation, request.PreferredEducation)
                .Set(p => p.PreferredOccupation, request.PreferredOccupation)
                .Set(p => p.MinAnnualIncome, request.MinAnnualIncome)
                .Set(p => p.PreferredCountries, request.PreferredCountries)
                .Set(p => p.PreferredStates, request.PreferredStates)
                .Set(p => p.PreferredCities, request.PreferredCities);

            await _context.PartnerPreferences.UpdateOneAsync(p => p.Id == existingPreference.Id, update);
            
            var updated = await _context.PartnerPreferences.Find(p => p.Id == existingPreference.Id).FirstOrDefaultAsync();
            return MapToPreferenceResponse(updated!);
        }
    }

    public async Task<PreferenceResponse?> GetPreferenceAsync(string userId)
    {
        var preference = await _context.PartnerPreferences.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        if (preference == null)
        {
            return null;
        }

        return MapToPreferenceResponse(preference);
    }

    public async Task<bool> DeletePreferenceAsync(string userId)
    {
        var result = await _context.PartnerPreferences.DeleteOneAsync(p => p.UserId == userId);
        return result.DeletedCount > 0;
    }

    private PreferenceResponse MapToPreferenceResponse(PartnerPreference preference)
    {
        return new PreferenceResponse
        {
            Id = preference.Id,
            MinAge = preference.MinAge,
            MaxAge = preference.MaxAge,
            MinHeightInCm = preference.MinHeightInCm,
            MaxHeightInCm = preference.MaxHeightInCm,
            PreferredReligions = preference.PreferredReligions,
            PreferredCommunities = preference.PreferredCommunities,
            PreferredMaritalStatus = preference.PreferredMaritalStatus,
            PreferredEducation = preference.PreferredEducation,
            PreferredOccupation = preference.PreferredOccupation,
            MinAnnualIncome = preference.MinAnnualIncome,
            PreferredCountries = preference.PreferredCountries,
            PreferredStates = preference.PreferredStates,
            PreferredCities = preference.PreferredCities
        };
    }
}
