using MatrimonyAPI.DTOs;

namespace MatrimonyAPI.Services;

public interface ISearchService
{
    Task<SearchResponse> SearchProfilesAsync(SearchRequest request, string currentUserId);
    Task<SearchResponse> GetMatchesAsync(string userId, int pageNumber, int pageSize);
}

