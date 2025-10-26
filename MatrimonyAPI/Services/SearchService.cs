using MongoDB.Driver;
using MatrimonyAPI.Data;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Models;

namespace MatrimonyAPI.Services;

public class SearchService : ISearchService
{
    private readonly MongoDbContext _context;

    public SearchService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<SearchResponse> SearchProfilesAsync(SearchRequest request, string currentUserId)
    {
        var filterBuilder = Builders<Profile>.Filter;
        var filters = new List<FilterDefinition<Profile>>
        {
            filterBuilder.Ne(p => p.UserId, currentUserId),
            filterBuilder.Eq(p => p.IsProfileComplete, true)
        };

        // Apply gender filter
        if (request.Gender.HasValue)
        {
            filters.Add(filterBuilder.Eq(p => p.Gender, request.Gender.Value));
        }

        // Apply age filters
        if (request.MinAge.HasValue || request.MaxAge.HasValue)
        {
            var now = DateTime.UtcNow;
            if (request.MaxAge.HasValue)
            {
                var minBirthDate = now.AddYears(-request.MaxAge.Value - 1);
                filters.Add(filterBuilder.Gte(p => p.DateOfBirth, minBirthDate));
            }
            if (request.MinAge.HasValue)
            {
                var maxBirthDate = now.AddYears(-request.MinAge.Value);
                filters.Add(filterBuilder.Lte(p => p.DateOfBirth, maxBirthDate));
            }
        }

        // Apply height filters
        if (request.MinHeightInCm.HasValue)
        {
            filters.Add(filterBuilder.Gte(p => p.HeightInCm, request.MinHeightInCm.Value));
        }
        if (request.MaxHeightInCm.HasValue)
        {
            filters.Add(filterBuilder.Lte(p => p.HeightInCm, request.MaxHeightInCm.Value));
        }

        // Apply religion filter
        if (!string.IsNullOrEmpty(request.Religion))
        {
            filters.Add(filterBuilder.Regex(p => p.Religion, new MongoDB.Bson.BsonRegularExpression(request.Religion, "i")));
        }

        // Apply community filter
        if (!string.IsNullOrEmpty(request.Community))
        {
            filters.Add(filterBuilder.Regex(p => p.Community, new MongoDB.Bson.BsonRegularExpression(request.Community, "i")));
        }

        // Apply marital status filter
        if (request.MaritalStatus.HasValue)
        {
            filters.Add(filterBuilder.Eq(p => p.MaritalStatus, request.MaritalStatus.Value));
        }

        // Apply education filter
        if (!string.IsNullOrEmpty(request.Education))
        {
            filters.Add(filterBuilder.Regex(p => p.Education, new MongoDB.Bson.BsonRegularExpression(request.Education, "i")));
        }

        // Apply occupation filter
        if (!string.IsNullOrEmpty(request.Occupation))
        {
            filters.Add(filterBuilder.Regex(p => p.Occupation, new MongoDB.Bson.BsonRegularExpression(request.Occupation, "i")));
        }

        // Apply annual income filter
        if (request.MinAnnualIncome.HasValue)
        {
            filters.Add(filterBuilder.Gte(p => p.AnnualIncome, request.MinAnnualIncome.Value));
        }

        // Apply location filters
        if (!string.IsNullOrEmpty(request.Country))
        {
            filters.Add(filterBuilder.Regex(p => p.Country, new MongoDB.Bson.BsonRegularExpression(request.Country, "i")));
        }
        if (!string.IsNullOrEmpty(request.State))
        {
            filters.Add(filterBuilder.Regex(p => p.State, new MongoDB.Bson.BsonRegularExpression(request.State, "i")));
        }
        if (!string.IsNullOrEmpty(request.City))
        {
            filters.Add(filterBuilder.Regex(p => p.City, new MongoDB.Bson.BsonRegularExpression(request.City, "i")));
        }

        var combinedFilter = filterBuilder.And(filters);

        // Get total count
        var totalCount = await _context.Profiles.CountDocumentsAsync(combinedFilter);

        // Get paginated results
        var profiles = await _context.Profiles
            .Find(combinedFilter)
            .SortByDescending(p => p.UpdatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync();

        var profileSummaries = new List<ProfileSummary>();
        foreach (var profile in profiles)
        {
            var primaryPhoto = await _context.Photos
                .Find(p => p.UserId == profile.UserId && p.IsPrimary && p.IsApproved)
                .FirstOrDefaultAsync();

            profileSummaries.Add(MapToProfileSummary(profile, primaryPhoto?.Url));
        }

        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        return new SearchResponse
        {
            Profiles = profileSummaries,
            TotalCount = (int)totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalPages = totalPages
        };
    }

    public async Task<SearchResponse> GetMatchesAsync(string userId, int pageNumber, int pageSize)
    {
        var profile = await _context.Profiles.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        var preference = await _context.PartnerPreferences.Find(p => p.UserId == userId).FirstOrDefaultAsync();

        if (profile == null || preference == null)
        {
            return new SearchResponse
            {
                Profiles = new List<ProfileSummary>(),
                TotalCount = 0,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = 0
            };
        }

        // Build search request from preferences
        var searchRequest = new SearchRequest
        {
            Gender = profile.Gender == Gender.Male ? Gender.Female : Gender.Male,
            MinAge = preference.MinAge,
            MaxAge = preference.MaxAge,
            MinHeightInCm = preference.MinHeightInCm,
            MaxHeightInCm = preference.MaxHeightInCm,
            MinAnnualIncome = preference.MinAnnualIncome,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return await SearchProfilesAsync(searchRequest, userId);
    }

    private ProfileSummary MapToProfileSummary(Profile profile, string? primaryPhotoUrl)
    {
        var age = DateTime.UtcNow.Year - profile.DateOfBirth.Year;
        if (DateTime.UtcNow < profile.DateOfBirth.AddYears(age))
        {
            age--;
        }

        return new ProfileSummary
        {
            Id = profile.Id,
            UserId = profile.UserId,
            Name = $"{profile.FirstName} {profile.LastName}",
            Age = age,
            Gender = profile.Gender,
            Religion = profile.Religion,
            Community = profile.Community,
            MaritalStatus = profile.MaritalStatus,
            HeightInCm = profile.HeightInCm,
            Education = profile.Education,
            Occupation = profile.Occupation,
            City = profile.City,
            State = profile.State,
            Country = profile.Country,
            PrimaryPhotoUrl = primaryPhotoUrl
        };
    }
}
