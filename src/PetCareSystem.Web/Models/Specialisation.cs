namespace PetCareSystem.Web.Models;

public class Specialisation
{
    public int SpecialisationId { get; set; }

    // Department this specialisation belongs to
    public int DepartmentId { get; set; }

    public string SpecialisationName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation property
    public Department Department { get; set; } = null!;
}