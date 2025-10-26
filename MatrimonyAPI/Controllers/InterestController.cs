using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Services;
using System.Security.Claims;

namespace MatrimonyAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InterestController : ControllerBase
{
    private readonly IInterestService _interestService;

    public InterestController(IInterestService interestService)
    {
        _interestService = interestService;
    }

    [HttpPost]
    public async Task<IActionResult> SendInterest([FromBody] SendInterestRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _interestService.SendInterestAsync(userId, request);

        if (result == null)
        {
            return BadRequest(new { message = "Failed to send interest. User not found or interest already exists." });
        }

        return Ok(result);
    }

    [HttpPost("respond")]
    public async Task<IActionResult> RespondToInterest([FromBody] RespondInterestRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _interestService.RespondToInterestAsync(userId, request);

        if (result == null)
        {
            return BadRequest(new { message = "Failed to respond to interest" });
        }

        return Ok(result);
    }

    [HttpGet("sent")]
    public async Task<IActionResult> GetSentInterests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _interestService.GetSentInterestsAsync(userId);
        return Ok(result);
    }

    [HttpGet("received")]
    public async Task<IActionResult> GetReceivedInterests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _interestService.GetReceivedInterestsAsync(userId);
        return Ok(result);
    }

    [HttpDelete("{interestId}")]
    public async Task<IActionResult> CancelInterest(string interestId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _interestService.CancelInterestAsync(userId, interestId);

        if (!result)
        {
            return BadRequest(new { message = "Failed to cancel interest" });
        }

        return Ok(new { message = "Interest cancelled successfully" });
    }
}

