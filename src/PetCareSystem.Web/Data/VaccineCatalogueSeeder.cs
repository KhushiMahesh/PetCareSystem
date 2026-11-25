using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Data;

public static class VaccineCatalogueSeeder
{
    private sealed record VaccineSeed(
        string Species,
        string Name,
        string Description,
        string DiseasePrevented,
        string RecommendedAge,
        string RecommendedDose,
        string AdministrationRoute,
        string RecommendedInterval,
        string SourceName,
        string SourceReference,
        string VerificationStatus
    );

    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var source = "WSAVA 2024 Vaccination Guidelines";
        var reference =
            "https://wsava.org/wp-content/uploads/2024/04/WSAVA-Vaccination-guidelines-2024.pdf";

        var vaccineData = new[]
        {
            new VaccineSeed(
                "Dog",
                "Canine DHPPi",
                "Combination canine core vaccine.",
                "Canine distemper, adenovirus-related infectious hepatitis, parvovirus and parainfluenza.",
                "Start no earlier than 6 weeks. Repeat every 2-4 weeks until the final puppy dose is given at 16 weeks of age or older.",
                "Not Verified - follow the specific manufacturer's product label.",
                "Parenteral; exact route depends on the licensed product.",
                "Core revaccination at or after 26 weeks of age; subsequent interval depends on vaccine type and veterinary guidance.",
                source,
                reference,
                "Partially Verified"
            ),

            new VaccineSeed(
                "Dog",
                "Rabies",
                "Rabies vaccination for dogs in rabies-endemic regions.",
                "Rabies virus infection.",
                "Not Verified - determine starting age from the locally licensed product label and applicable requirements.",
                "Not Verified - product-specific.",
                "Not Verified - follow the licensed product label.",
                "Follow applicable Indian requirements and the licensed product label.",
                source,
                reference,
                "Partially Verified"
            ),

            new VaccineSeed(
                "Dog",
                "Leptospirosis",
                "Risk-based or regionally recommended canine vaccination.",
                "Canine leptospirosis.",
                "Not Verified - depends on product, local disease risk and veterinary assessment.",
                "Not Verified - product-specific.",
                "Not Verified - follow the licensed product label.",
                "Not Verified - product-specific primary series and boosters.",
                source,
                reference,
                "Partially Verified"
            ),

            new VaccineSeed(
                "Cat",
                "Feline FVRCP",
                "Combination feline core vaccine.",
                "Feline panleukopenia, feline calicivirus and feline herpesvirus-1.",
                "Start no earlier than 6 weeks. Repeat every 2-4 weeks until the final kitten dose is given at 16 weeks of age or older.",
                "Not Verified - follow the specific manufacturer's product label.",
                "Parenteral; exact route depends on the licensed product.",
                "Core revaccination at or after 26 weeks of age; subsequent interval depends on vaccine type and veterinary guidance.",
                source,
                reference,
                "Partially Verified"
            ),

            new VaccineSeed(
                "Cat",
                "Rabies",
                "Rabies vaccination for cats in rabies-endemic regions.",
                "Rabies virus infection.",
                "Not Verified - determine starting age from the locally licensed product label and applicable requirements.",
                "Not Verified - product-specific.",
                "Not Verified - follow the licensed product label.",
                "Follow applicable Indian requirements and the licensed product label.",
                source,
                reference,
                "Partially Verified"
            ),

            new VaccineSeed(
                "Cat",
                "Feline Leukemia (FeLV)",
                "Risk-based vaccination against feline leukemia virus.",
                "Feline leukemia virus infection and associated disease.",
                "Consider for cats under 1 year and older cats with outdoor access or exposure to cats with outdoor access. Exact starting age requires product-label verification.",
                "Not Verified - product-specific.",
                "Not Verified - follow the licensed product label.",
                "Not Verified - product-specific primary series and boosters.",
                source,
                reference,
                "Partially Verified"
            )
        };

        foreach (var item in vaccineData)
        {
            var species = await context.Species
                .FirstOrDefaultAsync(s => s.SpeciesName == item.Species);

            if (species == null)
            {
                throw new InvalidOperationException(
                    $"Species '{item.Species}' was not found."
                );
            }

            var vaccine = await context.Vaccines.FirstOrDefaultAsync(v =>
                v.SpeciesId == species.SpeciesId &&
                v.VaccineName == item.Name);

            if (vaccine?.ReviewStatus == "Approved")
            {
                continue;
            }

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
            vaccine.RecommendedAge = item.RecommendedAge;
            vaccine.RecommendedDose = item.RecommendedDose;
            vaccine.AdministrationRoute = item.AdministrationRoute;
            vaccine.RecommendedInterval = item.RecommendedInterval;
            vaccine.SourceName = item.SourceName;
            vaccine.SourceReference = item.SourceReference;
            vaccine.ResearchDate = DateOnly.FromDateTime(DateTime.UtcNow);
            vaccine.VerificationStatus = item.VerificationStatus;

            vaccine.ReviewStatus = "Pending Approval";
            vaccine.ApprovedByVeterinarianProfileId = null;
            vaccine.ApprovedAt = null;
            vaccine.IsActive = true;
        }

        await context.SaveChangesAsync();
    }
}
