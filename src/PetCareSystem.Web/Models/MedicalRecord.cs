namespace PetCareSystem.Web.Models;

public class MedicalRecord
{
    public int MedicalRecordId { get; set; }

    // Animal receiving treatment
    public int PetId { get; set; }

    // Related regular appointment, if applicable
    public int? AppointmentId { get; set; }

    // Related emergency case, if applicable
    public int? EmergencyCaseId { get; set; }

    // Veterinarian who treated the animal
    public int VeterinarianProfileId { get; set; }

    // Medical details
    public string Symptoms { get; set; } = string.Empty;

    public string Diagnosis { get; set; } = string.Empty;

    public string? TreatmentNotes { get; set; }

    public string? FollowUpInstructions { get; set; }

    public DateTime RecordDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Pet Pet { get; set; } = null!;

    public Appointment? Appointment { get; set; }

    public EmergencyCase? EmergencyCase { get; set; }

    public VeterinarianProfile Veterinarian { get; set; } = null!;
}