namespace PetCareSystem.Web.Models;

public class Breed
{
    public int BreedId { get; set; }

    // Links the breed to its species
    public int SpeciesId { get; set; }

    public string BreedName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Navigation property
    public Species Species { get; set; } = null!;
}