using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Services;
using System.Security.Claims;

namespace MatrimonyAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _profileService.CreateProfileAsync(userId, request);

        if (result == null)
        {
            return BadRequest(new { message = "Profile already exists or user not found" });
        }

        return Ok(result);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _profileService.GetUserProfileAsync(userId);

        if (result == null)
        {
            return NotFound(new { message = "Profile not found" });
        }

        return Ok(result);
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetProfile(string profileId)
    {
        var result = await _profileService.GetProfileAsync(profileId);

        if (result == null)
        {
            return NotFound(new { message = "Profile not found" });
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _profileService.UpdateProfileAsync(userId, request);

        if (result == null)
        {
            return NotFound(new { message = "Profile not found" });
        }

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _profileService.DeleteProfileAsync(userId);

        if (!result)
        {
            return NotFound(new { message = "Profile not found" });
        }

        return Ok(new { message = "Profile deleted successfully" });
    }

    [HttpPost("photos")]
    public async Task<IActionResult> UploadPhoto([FromBody] UploadPhotoRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _profileService.UploadPhotoAsync(userId, request);

        if (result == null)
        {
            return BadRequest(new { message = "Failed to upload photo" });
        }

        return Ok(result);
    }

    [HttpDelete("photos/{photoId}")]
    public async Task<IActionResult> DeletePhoto(string photoId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _profileService.DeletePhotoAsync(userId, photoId);

        if (!result)
        {
            return NotFound(new { message = "Photo not found" });
        }

        return Ok(new { message = "Photo deleted successfully" });
    }

    [HttpPut("photos/{photoId}/set-primary")]
    public async Task<IActionResult> SetPrimaryPhoto(string photoId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _profileService.SetPrimaryPhotoAsync(userId, photoId);

        if (!result)
        {
            return NotFound(new { message = "Photo not found" });
        }

        return Ok(new { message = "Primary photo set successfully" });
    }
}

