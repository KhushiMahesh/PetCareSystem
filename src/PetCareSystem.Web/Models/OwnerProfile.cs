namespace PetCareSystem.Web.Models;

public class OwnerProfile
{
    public int OwnerProfileId { get; set; }

    // Links this profile to the user's login account
    public string UserId { get; set; } = string.Empty;

    // Owner details
    public string FullName { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}