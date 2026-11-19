namespace PetCareSystem.Web.Models;

public class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    // A department can have multiple specialisations
    public ICollection<Specialisation> Specialisations { get; set; }
        = new List<Specialisation>();
}