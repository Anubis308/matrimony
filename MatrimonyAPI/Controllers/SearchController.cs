using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Services;
using System.Security.Claims;

namespace MatrimonyAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody] SearchRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _searchService.SearchProfilesAsync(request, userId);
        return Ok(result);
    }

    [HttpGet("matches")]
    public async Task<IActionResult> GetMatches([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _searchService.GetMatchesAsync(userId, pageNumber, pageSize);
        return Ok(result);
    }
}

