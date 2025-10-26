using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Services;
using System.Security.Claims;

namespace MatrimonyAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PreferenceController : ControllerBase
{
    private readonly IPreferenceService _preferenceService;

    public PreferenceController(IPreferenceService preferenceService)
    {
        _preferenceService = preferenceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdatePreference([FromBody] PreferenceRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _preferenceService.CreateOrUpdatePreferenceAsync(userId, request);

        if (result == null)
        {
            return BadRequest(new { message = "Failed to save preference. Profile not found." });
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPreference()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _preferenceService.GetPreferenceAsync(userId);

        if (result == null)
        {
            return NotFound(new { message = "Preference not found" });
        }

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePreference()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _preferenceService.DeletePreferenceAsync(userId);

        if (!result)
        {
            return NotFound(new { message = "Preference not found" });
        }

        return Ok(new { message = "Preference deleted successfully" });
    }
}

