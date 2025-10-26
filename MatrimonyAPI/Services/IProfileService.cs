using MatrimonyAPI.DTOs;

namespace MatrimonyAPI.Services;

public interface IProfileService
{
    Task<ProfileResponse?> CreateProfileAsync(string userId, CreateProfileRequest request);
    Task<ProfileResponse?> GetProfileAsync(string profileId);
    Task<ProfileResponse?> GetUserProfileAsync(string userId);
    Task<ProfileResponse?> UpdateProfileAsync(string userId, UpdateProfileRequest request);
    Task<bool> DeleteProfileAsync(string userId);
    Task<PhotoResponse?> UploadPhotoAsync(string userId, UploadPhotoRequest request);
    Task<bool> DeletePhotoAsync(string userId, string photoId);
    Task<bool> SetPrimaryPhotoAsync(string userId, string photoId);
}

