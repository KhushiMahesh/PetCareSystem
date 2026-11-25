namespace PetCareSystem.Web.Models;

public class Pet
{
    public int PetId { get; set; }

    public int OwnerProfileId { get; set; }

    public string PetName { get; set; } = string.Empty;

    public int SpeciesId { get; set; }

    public int? BreedId { get; set; }

    public string? Gender { get; set; }

    // Use this when the exact date of birth is known.
    public DateOnly? DateOfBirth { get; set; }

    // Use these when only an estimated age is known.
    // Age is stored in months and calculated forward from AgeEstimateRecordedAt.
    public int? EstimatedAgeMonths { get; set; }

    public DateOnly? AgeEstimateRecordedAt { get; set; }

    public bool IsAgeEstimated { get; set; }

    public decimal? Weight { get; set; }

    public string? Colour { get; set; }

    public string? MicrochipNumber { get; set; }

    public string? MedicalNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public OwnerProfile Owner { get; set; } = null!;

    public Species Species { get; set; } = null!;

    public Breed? Breed { get; set; }
}