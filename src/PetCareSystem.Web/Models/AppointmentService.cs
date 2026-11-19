namespace PetCareSystem.Web.Models;

public class AppointmentService
{
    public int AppointmentServiceId { get; set; }

    public int AppointmentId { get; set; }

    public int ServiceId { get; set; }

    public int Quantity { get; set; } = 1;

    public decimal UnitPrice { get; set; }

    // Navigation properties
    public Appointment Appointment { get; set; } = null!;

    public Service Service { get; set; } = null!;
}