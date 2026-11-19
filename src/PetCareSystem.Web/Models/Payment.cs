namespace PetCareSystem.Web.Models;

public class Payment
{
    public int PaymentId { get; set; }

    public int InvoiceId { get; set; }

    public decimal Amount { get; set; }

    // Cash, UPI, Card, Online
    public string PaymentMethod { get; set; } = string.Empty;

    // Pending, Successful, Failed, Refunded
    public string PaymentStatus { get; set; } = "Pending";

    public string? TransactionReference { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Invoice Invoice { get; set; } = null!;
}