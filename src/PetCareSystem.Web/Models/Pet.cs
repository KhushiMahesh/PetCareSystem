namespace PetCareSystem.Web.Models;

public class Pet
{
    public int PetId { get; set; }

    // Owner of the animal
    public int OwnerProfileId { get; set; }

    // Animal details
    public string PetName { get; set; } = string.Empty;

    public int SpeciesId { get; set; }

    // Breed is optional because some animals may be mixed or unknown
    public int? BreedId { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public decimal? Weight { get; set; }

    public string? Colour { get; set; }

    public string? MicrochipNumber { get; set; }

    public string? MedicalNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public OwnerProfile Owner { get; set; } = null!;

    public Species Species { get; set; } = null!;

    public Breed? Breed { get; set; }
}