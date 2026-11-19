namespace PetCareSystem.Web.Models;

public class Invoice
{
    public int InvoiceId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public int OwnerProfileId { get; set; }

    // Invoice can belong to an appointment or emergency
    public int? AppointmentId { get; set; }

    public int? EmergencyCaseId { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TotalAmount { get; set; }

    // Pending, PartiallyPaid, Paid, Cancelled
    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public OwnerProfile Owner { get; set; } = null!;

    public Appointment? Appointment { get; set; }

    public EmergencyCase? EmergencyCase { get; set; }
}