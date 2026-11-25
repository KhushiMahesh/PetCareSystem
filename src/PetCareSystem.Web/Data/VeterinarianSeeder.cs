using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Data;

public static class VeterinarianSeeder
{
    private sealed record DoctorDefinition(
        string Name,
        string Qualification,
        decimal Fee,
        string Specialisation,
        string[] Species);

    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        var initialPin = configuration["DoctorSeed:InitialPin"];

        if (string.IsNullOrWhiteSpace(initialPin) ||
            initialPin.Length != 4 ||
            !initialPin.All(char.IsDigit))
        {
            throw new InvalidOperationException(
                "DoctorSeed:InitialPin must contain exactly four digits. " +
                "Configure it using .NET User Secrets.");
        }

        var doctors = new List<DoctorDefinition>
        {
            new("Dr. Aarav Sharma", "BVSc & AH", 500m,
                "General Consultation",
                ["Dog", "Cat", "Rabbit", "Guinea Pig", "Hamster"]),

            new("Dr. Ananya Rao", "MVSc Veterinary Medicine", 550m,
                "Preventive Healthcare",
                ["Dog", "Cat", "Rabbit", "Guinea Pig", "Hamster"]),

            new("Dr. Rohan Mehta", "MVSc Internal Medicine", 700m,
                "Internal Medicine",
                ["Dog", "Cat", "Rabbit", "Guinea Pig", "Hamster"]),

            new("Dr. Priya Nair", "MVSc Veterinary Surgery", 900m,
                "Soft Tissue Surgery",
                ["Dog", "Cat", "Rabbit"]),

            new("Dr. Arjun Reddy", "MVSc General Surgery", 850m,
                "General Surgery",
                ["Dog", "Cat", "Rabbit"]),

            new("Dr. Kavya Iyer", "MVSc Orthopaedics", 950m,
                "Orthopaedic Surgery",
                ["Dog", "Cat", "Horse"]),

            new("Dr. Vikram Joshi", "MVSc Veterinary Orthopaedics", 850m,
                "Bone and Joint Care",
                ["Dog", "Cat", "Horse"]),

            new("Dr. Meera Menon", "MVSc Veterinary Gynaecology", 800m,
                "Veterinary Gynaecology",
                ["Dog", "Cat", "Cattle", "Buffalo", "Goat"]),

            new("Dr. Aditya Kulkarni", "MVSc Veterinary Obstetrics", 850m,
                "Obstetrics",
                ["Cattle", "Buffalo", "Sheep", "Goat"]),

            new("Dr. Sneha Pillai", "MVSc Animal Reproduction", 800m,
                "Reproductive Health",
                ["Dog", "Cat", "Cattle", "Buffalo", "Sheep", "Goat"]),

            new("Dr. Rahul Verma", "MVSc Veterinary Dermatology", 700m,
                "Veterinary Dermatology",
                ["Dog", "Cat", "Rabbit"]),

            new("Dr. Divya Krishnan", "MVSc Veterinary Dermatology", 650m,
                "Skin and Allergy Care",
                ["Dog", "Cat", "Rabbit"]),

            new("Dr. Karthik Bhat", "MVSc Veterinary Cardiology", 900m,
                "Veterinary Cardiology",
                ["Dog", "Cat"]),

            new("Dr. Nisha Kapoor", "MVSc Veterinary Ophthalmology", 850m,
                "Veterinary Ophthalmology",
                ["Dog", "Cat", "Horse", "Parrot"]),

            new("Dr. Sandeep Rao", "MVSc Veterinary Dentistry", 750m,
                "Veterinary Dentistry",
                ["Dog", "Cat", "Rabbit", "Horse"]),

            new("Dr. Pooja Desai", "MVSc Veterinary Oral Surgery", 950m,
                "Oral Surgery",
                ["Dog", "Cat", "Rabbit"]),

            new("Dr. Manish Gupta", "MVSc Animal Reproduction", 800m,
                "Animal Reproduction",
                ["Cattle", "Buffalo", "Sheep", "Goat", "Pig"]),

            new("Dr. Aishwarya Shetty", "MVSc Breeding Management", 750m,
                "Breeding Management",
                ["Cattle", "Buffalo", "Sheep", "Goat", "Pig"]),

            new("Dr. Naveen Kumar", "MVSc Large Animal Medicine", 650m,
                "Cattle Medicine",
                ["Cattle"]),

            new("Dr. Lakshmi Prasad", "MVSc Large Animal Medicine", 650m,
                "Buffalo Medicine",
                ["Buffalo"]),

            new("Dr. Harish Patil", "MVSc Veterinary Medicine", 600m,
                "Sheep and Goat Medicine",
                ["Sheep", "Goat"]),

            new("Dr. Swati Mishra", "MVSc Veterinary Medicine", 650m,
                "Pig Medicine",
                ["Pig"]),

            new("Dr. Neha Sinha", "MVSc Avian Medicine", 700m,
                "Avian Medicine",
                ["Parrot", "Pigeon", "Chicken", "Duck", "Goose", "Turkey"]),

            new("Dr. Abhishek Das", "MVSc Poultry Medicine", 650m,
                "Poultry Medicine",
                ["Chicken", "Duck", "Goose", "Turkey"]),

            new("Dr. Rohit Malhotra", "MVSc Equine Medicine", 850m,
                "Equine Medicine",
                ["Horse", "Donkey"]),

            new("Dr. Tanvi Shah", "MVSc Equine Orthopaedics", 950m,
                "Horse Orthopaedics",
                ["Horse"]),

            new("Dr. Siddharth Rao", "MVSc Emergency Veterinary Medicine", 1000m,
                "Emergency Medicine",
                ["Dog", "Cat", "Rabbit", "Cattle", "Buffalo", "Horse"]),

            new("Dr. Ritu Agarwal", "MVSc Veterinary Critical Care", 1100m,
                "Critical Care",
                ["Dog", "Cat", "Rabbit", "Cattle", "Buffalo", "Horse"]),

            new("Dr. Gaurav Singh", "MVSc Veterinary Microbiology", 800m,
                "Veterinary Infectious Diseases",
                ["Dog", "Cat", "Cattle", "Buffalo", "Sheep", "Goat",
                 "Pig", "Horse", "Chicken", "Duck"]),

            new("Dr. Shreya Bose", "MVSc Veterinary Parasitology", 750m,
                "Parasitology",
                ["Dog", "Cat", "Rabbit", "Guinea Pig", "Hamster",
                 "Cattle", "Buffalo", "Sheep", "Goat", "Pig",
                 "Horse", "Donkey", "Chicken", "Duck", "Goose",
                 "Turkey", "Parrot", "Pigeon"])
        };

        if (doctors.Count != 30)
        {
            throw new InvalidOperationException(
                "The veterinarian seed must contain exactly 30 doctors.");
        }

        var specialisations = await context.Specialisations
            .Where(s => s.IsActive)
            .ToDictionaryAsync(
                s => s.SpecialisationName,
                s => s.SpecialisationId);

        var species = await context.Species
            .Where(s => s.IsActive)
            .ToDictionaryAsync(
                s => s.SpeciesName,
                s => s.SpeciesId);

        for (int i = 0; i < doctors.Count; i++)
        {
            var definition = doctors[i];
            var doctorId = $"D{i + 1:D3}";

            if (!specialisations.TryGetValue(
                    definition.Specialisation,
                    out var specialisationId))
            {
                throw new InvalidOperationException(
                    $"Specialisation not found: {definition.Specialisation}");
            }

            foreach (var speciesName in definition.Species)
            {
                if (!species.ContainsKey(speciesName))
                {
                    throw new InvalidOperationException(
                        $"Species not found: {speciesName}");
                }
            }

            var user = await userManager.FindByNameAsync(doctorId);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = doctorId,
                    Email = null,
                    EmailConfirmed = true
                };

                user.PasswordHash =
                    userManager.PasswordHasher.HashPassword(
                        user,
                        initialPin);

                var createResult = await userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(
                        "; ",
                        createResult.Errors.Select(e => e.Description));

                    throw new InvalidOperationException(
                        $"Could not create doctor {doctorId}: {errors}");
                }
            }

            else
            {
                if (!await userManager.CheckPasswordAsync(user, initialPin))
                {
                    user.PasswordHash =
                        userManager.PasswordHasher.HashPassword(
                            user,
                            initialPin);

                    var updateResult = await userManager.UpdateAsync(user);

                    if (!updateResult.Succeeded)
                    {
                        var errors = string.Join(
                            "; ",
                            updateResult.Errors.Select(e => e.Description));

                        throw new InvalidOperationException(
                            $"Could not update PIN for doctor {doctorId}: {errors}");
                    }
                }
            }
            if (!await userManager.IsInRoleAsync(user, "Veterinarian"))
            {
                var roleResult = await userManager.AddToRoleAsync(
                    user,
                    "Veterinarian");

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(
                        "; ",
                        roleResult.Errors.Select(e => e.Description));

                    throw new InvalidOperationException(
                        $"Could not assign Veterinarian role to {doctorId}: {errors}");
                }
            }

            var profile = await context.VeterinarianProfiles
                .FirstOrDefaultAsync(v => v.UserId == user.Id);

            if (profile == null)
            {
                profile = new VeterinarianProfile
                {
                    UserId = user.Id,
                    FullName = definition.Name,
                    Qualification = definition.Qualification,
                    ProfessionalRegistrationNumber =
                        $"DEMO-VET-{i + 1:D3}",
                    ConsultationFee = definition.Fee,
                    IsActive = true
                };

                context.VeterinarianProfiles.Add(profile);

                await context.SaveChangesAsync();
            }

            var existingSpecialisations =
                await context.VeterinarianSpecialisations
                    .Where(v => v.VeterinarianProfileId ==
                                profile.VeterinarianProfileId)
                    .ToListAsync();

            if (existingSpecialisations.Count > 1 ||
                existingSpecialisations.Any(
                    v => v.SpecialisationId != specialisationId))
            {
                throw new InvalidOperationException(
                    $"Doctor {doctorId} has an unexpected specialisation assignment.");
            }

            if (existingSpecialisations.Count == 0)
            {
                context.VeterinarianSpecialisations.Add(
                    new VeterinarianSpecialisation
                    {
                        VeterinarianProfileId =
                            profile.VeterinarianProfileId,
                        SpecialisationId = specialisationId
                    });
            }

            foreach (var speciesName in definition.Species)
            {
                var speciesId = species[speciesName];

                var exists = await context.VeterinarianSpecies.AnyAsync(
                    v => v.VeterinarianProfileId ==
                         profile.VeterinarianProfileId &&
                         v.SpeciesId == speciesId);

                if (!exists)
                {
                    context.VeterinarianSpecies.Add(
                        new VeterinarianSpecies
                        {
                            VeterinarianProfileId =
                                profile.VeterinarianProfileId,
                            SpeciesId = speciesId
                        });
                }
            }

            var schedules = new[]
            {
                new { Day = DayOfWeek.Monday,
                      Start = new TimeOnly(9, 0),
                      End = new TimeOnly(17, 0) },

                new { Day = DayOfWeek.Tuesday,
                      Start = new TimeOnly(9, 0),
                      End = new TimeOnly(17, 0) },

                new { Day = DayOfWeek.Wednesday,
                      Start = new TimeOnly(9, 0),
                      End = new TimeOnly(17, 0) },

                new { Day = DayOfWeek.Thursday,
                      Start = new TimeOnly(9, 0),
                      End = new TimeOnly(17, 0) },

                new { Day = DayOfWeek.Friday,
                      Start = new TimeOnly(9, 0),
                      End = new TimeOnly(17, 0) },

                new { Day = DayOfWeek.Saturday,
                      Start = new TimeOnly(9, 0),
                      End = new TimeOnly(13, 0) }
            };

            foreach (var schedule in schedules)
            {
                var exists = await context.VeterinarianSchedules.AnyAsync(
                    v => v.VeterinarianProfileId ==
                         profile.VeterinarianProfileId &&
                         v.DayOfWeek == schedule.Day);

                if (!exists)
                {
                    context.VeterinarianSchedules.Add(
                        new VeterinarianSchedule
                        {
                            VeterinarianProfileId =
                                profile.VeterinarianProfileId,
                            DayOfWeek = schedule.Day,
                            StartTime = schedule.Start,
                            EndTime = schedule.End,
                            IsAvailable = true
                        });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
