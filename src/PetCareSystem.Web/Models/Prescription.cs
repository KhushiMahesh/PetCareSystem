namespace PetCareSystem.Web.Models;

public class Prescription
{
    public int PrescriptionId { get; set; }

    public int MedicalRecordId { get; set; }

    public DateTime PrescribedAt { get; set; } = DateTime.UtcNow;

    public DateOnly? ValidUntil { get; set; }

    public string? GeneralInstructions { get; set; }

    // Navigation property
    public MedicalRecord MedicalRecord { get; set; } = null!;
}