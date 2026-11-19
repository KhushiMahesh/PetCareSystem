namespace PetCareSystem.Web.Models;

public class AppointmentStatusHistory
{
    public int AppointmentStatusHistoryId { get; set; }

    public int AppointmentId { get; set; }

    // Previous and updated statuses
    public string? PreviousStatus { get; set; }

    public string NewStatus { get; set; } = string.Empty;

    // Who changed the status
    public string? ChangedByUserId { get; set; }

    public string? Remarks { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Appointment Appointment { get; set; } = null!;
}