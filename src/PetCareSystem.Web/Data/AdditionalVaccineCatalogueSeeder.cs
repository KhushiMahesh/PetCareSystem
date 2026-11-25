using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Data;

public static class AdditionalVaccineCatalogueSeeder
{
    private sealed record VaccineSeed(
        string Species,
        string Name,
        string Description,
        string DiseasePrevented,
        string SourceName,
        string SourceReference
    );

    public static async Task SeedAsync(ApplicationDbContext context)
    {
        const string source = "ICAR - Vaccines for Livestock and Poultry";
        const string reference =
            "https://icar.gov.in/sites/default/files/2022-06/Vaccines-for-Livestock-and-Poultry.pdf";

        var vaccineData = new[]
        {
            new VaccineSeed("Cattle", "Foot-and-Mouth Disease (FMD)",
                "Inactivated vaccine listed in the ICAR livestock vaccine reference.",
                "Foot-and-mouth disease.", source, reference),

            new VaccineSeed("Cattle", "Haemorrhagic Septicaemia (HS)",
                "Vaccine listed in the ICAR livestock vaccine reference.",
                "Haemorrhagic septicaemia.", source, reference),

            new VaccineSeed("Cattle", "Black Quarter (BQ)",
                "Vaccine listed in the ICAR livestock vaccine reference.",
                "Black quarter.", source, reference),

            new VaccineSeed("Cattle", "Anthrax",
                "Anthrax spore vaccine listed in the ICAR reference.",
                "Anthrax.", source, reference),

            new VaccineSeed("Cattle", "Brucellosis (S-19)",
                "Brucella abortus S-19 vaccine. Use requires strict veterinary eligibility assessment.",
                "Brucellosis.", source, reference),

            new VaccineSeed("Buffalo", "Foot-and-Mouth Disease (FMD)",
                "Inactivated vaccine listed in the ICAR livestock vaccine reference.",
                "Foot-and-mouth disease.", source, reference),

            new VaccineSeed("Buffalo", "Haemorrhagic Septicaemia (HS)",
                "Vaccine listed in the ICAR livestock vaccine reference.",
                "Haemorrhagic septicaemia.", source, reference),

            new VaccineSeed("Buffalo", "Black Quarter (BQ)",
                "Vaccine listed in the ICAR livestock vaccine reference.",
                "Black quarter.", source, reference),

            new VaccineSeed("Buffalo", "Anthrax",
                "Anthrax spore vaccine listed in the ICAR reference.",
                "Anthrax.", source, reference),

            new VaccineSeed("Buffalo", "Buffalopox",
                "Live attenuated buffalopox vaccine listed in the ICAR reference.",
                "Buffalopox.", source, reference),

            new VaccineSeed("Sheep", "Peste des Petits Ruminants (PPR)",
                "Live attenuated PPR vaccine listed in the ICAR reference.",
                "Peste des petits ruminants.", source, reference),

            new VaccineSeed("Sheep", "Sheeppox",
                "Live attenuated sheeppox vaccine listed in the ICAR reference.",
                "Sheeppox.", source, reference),

            new VaccineSeed("Sheep", "Enterotoxaemia",
                "Enterotoxaemia vaccine listed in the ICAR reference.",
                "Enterotoxaemia.", source, reference),

            new VaccineSeed("Sheep", "Bluetongue",
                "Bluetongue vaccine listed in the ICAR reference. Applicability depends on local serotypes and veterinary advice.",
                "Bluetongue.", source, reference),

            new VaccineSeed("Sheep", "Orf",
                "Live attenuated Orf vaccine listed in the ICAR reference.",
                "Contagious ecthyma (Orf).", source, reference),

            new VaccineSeed("Goat", "Peste des Petits Ruminants (PPR)",
                "Live attenuated PPR vaccine listed in the ICAR reference.",
                "Peste des petits ruminants.", source, reference),

            new VaccineSeed("Goat", "Goatpox",
                "Goatpox vaccine listed in the ICAR reference.",
                "Goatpox.", source, reference),

            new VaccineSeed("Goat", "Enterotoxaemia",
                "Enterotoxaemia vaccine listed in the ICAR reference.",
                "Enterotoxaemia.", source, reference),

            new VaccineSeed("Goat", "Bluetongue",
                "Bluetongue vaccine listed in the ICAR reference. Applicability depends on local serotypes and veterinary advice.",
                "Bluetongue.", source, reference),

            new VaccineSeed("Goat", "Orf",
                "Live attenuated Orf vaccine listed in the ICAR reference.",
                "Contagious ecthyma (Orf).", source, reference),

            new VaccineSeed("Pig", "Classical Swine Fever",
                "Classical swine fever vaccine listed in the ICAR reference.",
                "Classical swine fever.", source, reference),

            new VaccineSeed("Horse", "Equine Herpesvirus-1",
                "Equine herpesvirus-1 vaccine listed in the ICAR reference.",
                "Equine herpesvirus-1 associated disease.", source, reference),

            new VaccineSeed("Horse", "Equine Influenza",
                "Equine influenza vaccine listed in the ICAR reference.",
                "Equine influenza.", source, reference),

            new VaccineSeed("Chicken", "Newcastle Disease (Ranikhet)",
                "Newcastle disease vaccine listed in the ICAR poultry reference.",
                "Newcastle disease (Ranikhet disease).", source, reference),

            new VaccineSeed("Chicken", "Fowlpox",
                "Fowlpox vaccine listed in the ICAR poultry reference.",
                "Fowlpox.", source, reference),

            new VaccineSeed("Chicken", "Infectious Bursal Disease (IBD)",
                "Infectious bursal disease vaccine listed in the ICAR poultry reference.",
                "Infectious bursal disease (Gumboro).", source, reference)
        };

        foreach (var item in vaccineData)
        {
            var species = await context.Species
                .FirstOrDefaultAsync(s => s.SpeciesName == item.Species);

            if (species == null)
                throw new InvalidOperationException(
                    $"Species '{item.Species}' was not found.");

            var vaccine = await context.Vaccines.FirstOrDefaultAsync(v =>
                v.SpeciesId == species.SpeciesId &&
                v.VaccineName == item.Name);

            if (vaccine?.ReviewStatus == "Approved")
                continue;

            if (vaccine == null)
            {
                vaccine = new Vaccine
                {
                    SpeciesId = species.SpeciesId,
                    VaccineName = item.Name
                };

                context.Vaccines.Add(vaccine);
            }

            vaccine.Description = item.Description;
            vaccine.DiseasePrevented = item.DiseasePrevented;
            vaccine.RecommendedAge =
                "Not specified in this catalogue entry. Veterinarian must assess the animal and consult the applicable product label.";
            vaccine.RecommendedDose =
                "Not specified. Confirm the licensed product label before administration.";
            vaccine.AdministrationRoute =
                "Not specified. Confirm the licensed product label.";
            vaccine.RecommendedInterval =
                "Not specified. Veterinarian must determine the schedule based on the animal, region and product.";

            vaccine.SourceName = item.SourceName;
            vaccine.SourceReference = item.SourceReference;
            vaccine.ResearchDate = DateOnly.FromDateTime(DateTime.UtcNow);
            vaccine.VerificationStatus = "Partially Verified";
            vaccine.ReviewStatus = "Pending Approval";
            vaccine.ApprovedByVeterinarianProfileId = null;
            vaccine.ApprovedAt = null;
            vaccine.IsActive = true;
        }

        await context.SaveChangesAsync();
    }
}
