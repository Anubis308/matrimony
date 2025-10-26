namespace MatrimonyAPI.DTOs;

public class PreferenceRequest
{
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public int? MinHeightInCm { get; set; }
    public int? MaxHeightInCm { get; set; }
    public string? PreferredReligions { get; set; }
    public string? PreferredCommunities { get; set; }
    public string? PreferredMaritalStatus { get; set; }
    public string? PreferredEducation { get; set; }
    public string? PreferredOccupation { get; set; }
    public decimal? MinAnnualIncome { get; set; }
    public string? PreferredCountries { get; set; }
    public string? PreferredStates { get; set; }
    public string? PreferredCities { get; set; }
}

public class PreferenceResponse
{
    public string Id { get; set; } = string.Empty;
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public int? MinHeightInCm { get; set; }
    public int? MaxHeightInCm { get; set; }
    public string? PreferredReligions { get; set; }
    public string? PreferredCommunities { get; set; }
    public string? PreferredMaritalStatus { get; set; }
    public string? PreferredEducation { get; set; }
    public string? PreferredOccupation { get; set; }
    public decimal? MinAnnualIncome { get; set; }
    public string? PreferredCountries { get; set; }
    public string? PreferredStates { get; set; }
    public string? PreferredCities { get; set; }
}

