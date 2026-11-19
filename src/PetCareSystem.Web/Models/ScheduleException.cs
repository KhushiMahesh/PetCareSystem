namespace PetCareSystem.Web.Models;

public class ScheduleException
{
    public int ScheduleExceptionId { get; set; }

    public int VeterinarianProfileId { get; set; }

    // Date of the exception
    public DateOnly ExceptionDate { get; set; }

    // Optional changed working hours
    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    // Reason for the exception
    public string Reason { get; set; } = string.Empty;

    // Whether the veterinarian is unavailable for the entire day
    public bool IsFullDayUnavailable { get; set; } = true;

    // Navigation property
    public VeterinarianProfile Veterinarian { get; set; } = null!;
}