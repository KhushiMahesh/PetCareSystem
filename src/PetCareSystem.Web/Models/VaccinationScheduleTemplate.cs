namespace PetCareSystem.Web.Models;

public class VaccinationScheduleTemplate
{
    public int VaccinationScheduleTemplateId { get; set; }

    public int SpeciesId { get; set; }

    public int VaccineId { get; set; }

    public string DoseName { get; set; } = string.Empty;

    public int DoseNumber { get; set; }

    // Recommended age for this dose, expressed in months.
    public int RecommendedAgeMonths { get; set; }

    // Optional precise recommended age in days.
    public int? RecommendedAgeDays { get; set; }

    // Optional acceptable age range.
    public int? MinimumAgeMonths { get; set; }

    public int? MaximumAgeMonths { get; set; }

    // Interval after the previous dose, if applicable.
    public int? IntervalAfterPreviousDoseMonths { get; set; }

    // Recommendations are region-specific.
    public string RegionCode { get; set; } = "IN";

    public string? SourceName { get; set; }

    public string? SourceReference { get; set; }

    public DateOnly? SourceReviewedOn { get; set; }

    // Veterinary approval is required before use.
    public bool RequiresVeterinarianApproval { get; set; } = true;

    // New templates remain unapproved by default.
    public bool IsApproved { get; set; } = false;

    public int? ApprovedByVeterinarianProfileId { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public Species Species { get; set; } = null!;

    public Vaccine Vaccine { get; set; } = null!;

    public VeterinarianProfile? ApprovedByVeterinarian { get; set; }
}

