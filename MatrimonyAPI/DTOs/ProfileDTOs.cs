using MatrimonyAPI.Models;

namespace MatrimonyAPI.DTOs;

public class CreateProfileRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string Community { get; set; } = string.Empty;
    public string MotherTongue { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public int HeightInCm { get; set; }
    public string Education { get; set; } = string.Empty;
    public string Occupation { get; set; } = string.Empty;
    public decimal? AnnualIncome { get; set; }
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string FamilyDetails { get; set; } = string.Empty;
    public string Hobbies { get; set; } = string.Empty;
}

public class UpdateProfileRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Religion { get; set; }
    public string? Community { get; set; }
    public string? MotherTongue { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public int? HeightInCm { get; set; }
    public string? Education { get; set; }
    public string? Occupation { get; set; }
    public decimal? AnnualIncome { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? About { get; set; }
    public string? FamilyDetails { get; set; }
    public string? Hobbies { get; set; }
}

public class ProfileResponse
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string Community { get; set; } = string.Empty;
    public string MotherTongue { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public int HeightInCm { get; set; }
    public string Education { get; set; } = string.Empty;
    public string Occupation { get; set; } = string.Empty;
    public decimal? AnnualIncome { get; set; }
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string FamilyDetails { get; set; } = string.Empty;
    public string Hobbies { get; set; } = string.Empty;
    public List<PhotoResponse> Photos { get; set; } = new();
    public bool IsProfileComplete { get; set; }
}

public class PhotoResponse
{
    public string Id { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public bool IsApproved { get; set; }
}

public class UploadPhotoRequest
{
    public string Url { get; set; } = string.Empty;
    public bool IsPrimary { get; set; } = false;
}

