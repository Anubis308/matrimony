using MatrimonyAPI.DTOs;

namespace MatrimonyAPI.Services;

public interface IPreferenceService
{
    Task<PreferenceResponse?> CreateOrUpdatePreferenceAsync(string userId, PreferenceRequest request);
    Task<PreferenceResponse?> GetPreferenceAsync(string userId);
    Task<bool> DeletePreferenceAsync(string userId);
}

