namespace PetCareSystem.Web.Models;

public class VeterinarianProfile
{
    public int VeterinarianProfileId { get; set; }

    // Links the veterinarian to their login account
    public string UserId { get; set; } = string.Empty;

    // Professional details
    public string FullName { get; set; } = string.Empty;

    public string? Qualification { get; set; }

    public string? ProfessionalRegistrationNumber { get; set; }

    public decimal ConsultationFee { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}