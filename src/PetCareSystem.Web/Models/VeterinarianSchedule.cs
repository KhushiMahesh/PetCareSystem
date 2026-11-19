namespace PetCareSystem.Web.Models;

public class VeterinarianSchedule
{
    public int VeterinarianScheduleId { get; set; }

    public int VeterinarianProfileId { get; set; }

    // Day of the week
    public DayOfWeek DayOfWeek { get; set; }

    // Working hours
    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool IsAvailable { get; set; } = true;

    // Navigation property
    public VeterinarianProfile Veterinarian { get; set; } = null!;
}