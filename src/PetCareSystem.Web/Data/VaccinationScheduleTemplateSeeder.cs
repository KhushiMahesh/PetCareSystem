using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Data;

public static class VaccinationScheduleTemplateSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var species = await context.Species
            .FirstOrDefaultAsync(s => s.SpeciesName == "Dog");

        if (species == null)
            return;

        var vaccine = await context.Vaccines.FirstOrDefaultAsync(v =>
            v.SpeciesId == species.SpeciesId &&
            v.VaccineName == "Canine DHPPi");

        if (vaccine == null)
            return;

        var doses = new[]
        {
            ("First Dose", 1, 42),
            ("Second Dose", 2, 63),
            ("Third Dose", 3, 84),
            ("Final Puppy Dose", 4, 112),
            ("Core Revaccination", 5, 182)
        };

        foreach (var dose in doses)
        {
            bool exists = await context.VaccinationScheduleTemplates.AnyAsync(t =>
                t.SpeciesId == species.SpeciesId &&
                t.VaccineId == vaccine.VaccineId &&
                t.DoseNumber == dose.Item2 &&
                t.RegionCode == "IN");

            if (exists)
                continue;

            context.VaccinationScheduleTemplates.Add(
                new VaccinationScheduleTemplate
                {
                    SpeciesId = species.SpeciesId,
                    VaccineId = vaccine.VaccineId,
                    DoseName = dose.Item1,
                    DoseNumber = dose.Item2,
                    RecommendedAgeMonths = 0,
                    RecommendedAgeDays = dose.Item3,
                    RegionCode = "IN",
                    SourceName = "WSAVA 2024 Vaccination Guidelines",
                    SourceReference = "https://wsava.org/wp-content/uploads/2024/04/WSAVA-Vaccination-guidelines-2024.pdf",
                    SourceReviewedOn = new DateOnly(2026, 11, 25),
                    RequiresVeterinarianApproval = true,
                    IsApproved = false,
                    IsActive = true
                });
        }

        await context.SaveChangesAsync();
    }
}
