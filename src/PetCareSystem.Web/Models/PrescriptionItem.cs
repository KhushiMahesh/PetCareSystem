namespace PetCareSystem.Web.Models;

public class PrescriptionItem
{
    public int PrescriptionItemId { get; set; }

    public int PrescriptionId { get; set; }

    public string MedicineName { get; set; } = string.Empty;

    public string Dosage { get; set; } = string.Empty;

    public string Frequency { get; set; } = string.Empty;

    public string? Duration { get; set; }

    public string? Instructions { get; set; }

    public int Quantity { get; set; }

    // Navigation property
    public Prescription Prescription { get; set; } = null!;
}