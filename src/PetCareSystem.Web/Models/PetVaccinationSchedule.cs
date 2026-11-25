namespace PetCareSystem.Web.Models;

public class PetVaccinationSchedule
{
    public int PetVaccinationScheduleId { get; set; }

    public int PetId { get; set; }

    public int VaccinationScheduleTemplateId { get; set; }

    // Date on which this dose is due.
    public DateOnly DueDate { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Pet Pet { get; set; } = null!;

    public VaccinationScheduleTemplate Template { get; set; } = null!;

    public VaccinationRecord? VaccinationRecord { get; set; }
}
