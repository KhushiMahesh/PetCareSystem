namespace PetCareSystem.Web.Models;

public class Vaccine
{
    public int VaccineId { get; set; }

    public string VaccineName { get; set; } = string.Empty;

    public int SpeciesId { get; set; }

    public string? Description { get; set; }

    public string? DiseasePrevented { get; set; }

    public string? RecommendedAge { get; set; }

    public string? RecommendedDose { get; set; }

    public string? AdministrationRoute { get; set; }

    public string? RecommendedInterval { get; set; }

    public string? SourceName { get; set; }

    public string? SourceReference { get; set; }

    public DateOnly? ResearchDate { get; set; }

    public string VerificationStatus { get; set; } = "Not Verified";

    public string ReviewStatus { get; set; } = "Pending Approval";

    public int? ApprovedByVeterinarianProfileId { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public Species Species { get; set; } = null!;

    public VeterinarianProfile? ApprovedByVeterinarian { get; set; }
}
