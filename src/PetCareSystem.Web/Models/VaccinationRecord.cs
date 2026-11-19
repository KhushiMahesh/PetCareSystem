namespace PetCareSystem.Web.Models;

public class VaccinationRecord
{
    public int VaccinationRecordId { get; set; }

    public int PetId { get; set; }

    public int VaccineId { get; set; }

    public int VeterinarianProfileId { get; set; }

    public DateOnly VaccinationDate { get; set; }

    public DateOnly? NextDueDate { get; set; }

    public string? BatchNumber { get; set; }

    public string? Notes { get; set; }

    // Navigation properties
    public Pet Pet { get; set; } = null!;

    public Vaccine Vaccine { get; set; } = null!;

    public VeterinarianProfile Veterinarian { get; set; } = null!;
}