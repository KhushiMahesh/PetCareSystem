namespace PetCareSystem.Web.Models;

public class EmergencyCase
{
    public int EmergencyCaseId { get; set; }

    // Animal requiring emergency treatment
    public int PetId { get; set; }

    // Owner bringing the animal
    public int OwnerProfileId { get; set; }

    // Veterinarian assigned to the case
    public int? VeterinarianProfileId { get; set; }

    // Emergency details
    public string Symptoms { get; set; } = string.Empty;

    public string? AdditionalNotes { get; set; }

    // Critical, High, Moderate, Low
    public string TriagePriority { get; set; } = "Moderate";

    // Waiting, Assigned, InTreatment, Completed, Transferred
    public string CaseStatus { get; set; } = "Waiting";

    public DateTime ArrivalTime { get; set; } = DateTime.UtcNow;

    // Staff member who registered the emergency
    public string? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Pet Pet { get; set; } = null!;

    public OwnerProfile Owner { get; set; } = null!;

    public VeterinarianProfile? Veterinarian { get; set; }
}