namespace PetCareSystem.Web.Models;

public class VeterinarianSpecialisation
{
    public int VeterinarianProfileId { get; set; }

    public int SpecialisationId { get; set; }

    // Navigation properties
    public VeterinarianProfile Veterinarian { get; set; } = null!;

    public Specialisation Specialisation { get; set; } = null!;
}