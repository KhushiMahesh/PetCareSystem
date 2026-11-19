namespace PetCareSystem.Web.Models;

public class VeterinarianSpecies
{
    public int VeterinarianProfileId { get; set; }

    public int SpeciesId { get; set; }

    // Navigation properties
    public VeterinarianProfile Veterinarian { get; set; } = null!;

    public Species Species { get; set; } = null!;
}