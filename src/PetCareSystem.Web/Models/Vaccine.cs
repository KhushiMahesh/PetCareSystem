namespace PetCareSystem.Web.Models;

public class Vaccine
{
    public int VaccineId { get; set; }

    public string VaccineName { get; set; } = string.Empty;

    public int SpeciesId { get; set; }

    public string? Description { get; set; }

    public string? RecommendedAge { get; set; }

    public string? RecommendedInterval { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation property
    public Species Species { get; set; } = null!;
}