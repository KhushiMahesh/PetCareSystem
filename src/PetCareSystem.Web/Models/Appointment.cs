namespace PetCareSystem.Web.Models;

public class Appointment
{
    public int AppointmentId { get; set; }

    // Who booked the appointment
    public int OwnerProfileId { get; set; }

    // Which pet needs treatment
    public int PetId { get; set; }

    // Assigned veterinarian
    public int VeterinarianProfileId { get; set; }

    // Requested specialisation, if applicable
    public int? SpecialisationId { get; set; }

    // Appointment date and time
    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    // Reason for the visit
    public string? Reason { get; set; }

    // Booking status
    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public OwnerProfile Owner { get; set; } = null!;

    public Pet Pet { get; set; } = null!;

    public VeterinarianProfile Veterinarian { get; set; } = null!;

    public Specialisation? Specialisation { get; set; }
}