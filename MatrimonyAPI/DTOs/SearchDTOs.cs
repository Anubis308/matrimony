using MatrimonyAPI.Models;

namespace MatrimonyAPI.DTOs;

public class SearchRequest
{
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public Gender? Gender { get; set; }
    public int? MinHeightInCm { get; set; }
    public int? MaxHeightInCm { get; set; }
    public string? Religion { get; set; }
    public string? Community { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? Education { get; set; }
    public string? Occupation { get; set; }
    public decimal? MinAnnualIncome { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class SearchResponse
{
    public List<ProfileSummary> Profiles { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}

public class ProfileSummary
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string Community { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public int HeightInCm { get; set; }
    public string Education { get; set; } = string.Empty;
    public string Occupation { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? PrimaryPhotoUrl { get; set; }
}

