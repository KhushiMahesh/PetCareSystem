namespace PetCareSystem.Web.Models;

public class Species
{
    public int SpeciesId { get; set; }

    public string SpeciesName { get; set; } = string.Empty;

    // Examples: Companion, Farm, Avian, Equine
    public string Category { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // A species can have multiple breeds
    public ICollection<Breed> Breeds { get; set; } = new List<Breed>();
}