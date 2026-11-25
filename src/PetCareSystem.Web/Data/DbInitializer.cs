
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // 1. DEPARTMENTS

        var departments = new[]
        {
            "General Veterinary Medicine",
            "Veterinary Surgery",
            "Veterinary Orthopaedics",
            "Veterinary Gynaecology and Obstetrics",
            "Veterinary Dermatology",
            "Veterinary Cardiology",
            "Veterinary Ophthalmology",
            "Veterinary Dentistry",
            "Veterinary Reproduction",
            "Large Animal Medicine",
            "Avian and Poultry Medicine",
            "Equine Medicine",
            "Emergency and Critical Care",
            "Infectious Diseases and Parasitology"
        };

        foreach (var name in departments)
        {
            if (!await context.Departments.AnyAsync(d =>
                d.DepartmentName == name))
            {
                context.Departments.Add(new Department
                {
                    DepartmentName = name,
                    IsActive = true
                });
            }
        }

        await context.SaveChangesAsync();


        // 2. SPECIALISATIONS

        var specialisations = new Dictionary<string, string[]>
        {
            ["General Veterinary Medicine"] =
            [
                "General Consultation",
                "Preventive Healthcare",
                "Internal Medicine"
            ],

            ["Veterinary Surgery"] =
            [
                "Soft Tissue Surgery",
                "General Surgery"
            ],

            ["Veterinary Orthopaedics"] =
            [
                "Orthopaedic Surgery",
                "Bone and Joint Care"
            ],

            ["Veterinary Gynaecology and Obstetrics"] =
            [
                "Veterinary Gynaecology",
                "Obstetrics",
                "Reproductive Health"
            ],

            ["Veterinary Dermatology"] =
            [
                "Veterinary Dermatology",
                "Skin and Allergy Care"
            ],

            ["Veterinary Cardiology"] =
            [
                "Veterinary Cardiology"
            ],

            ["Veterinary Ophthalmology"] =
            [
                "Veterinary Ophthalmology"
            ],

            ["Veterinary Dentistry"] =
            [
                "Veterinary Dentistry",
                "Oral Surgery"
            ],

            ["Veterinary Reproduction"] =
            [
                "Animal Reproduction",
                "Breeding Management"
            ],

            ["Large Animal Medicine"] =
            [
                "Cattle Medicine",
                "Buffalo Medicine",
                "Sheep and Goat Medicine",
                "Pig Medicine"
            ],

            ["Avian and Poultry Medicine"] =
            [
                "Avian Medicine",
                "Poultry Medicine"
            ],

            ["Equine Medicine"] =
            [
                "Equine Medicine",
                "Horse Orthopaedics"
            ],

            ["Emergency and Critical Care"] =
            [
                "Emergency Medicine",
                "Critical Care"
            ],

            ["Infectious Diseases and Parasitology"] =
            [
                "Veterinary Infectious Diseases",
                "Parasitology"
            ]
        };

        foreach (var departmentEntry in specialisations)
        {
            var department = await context.Departments
                .FirstAsync(d =>
                    d.DepartmentName == departmentEntry.Key);

            foreach (var name in departmentEntry.Value)
            {
                if (!await context.Specialisations.AnyAsync(s =>
                    s.DepartmentId == department.DepartmentId &&
                    s.SpecialisationName == name))
                {
                    context.Specialisations.Add(new Specialisation
                    {
                        DepartmentId = department.DepartmentId,
                        SpecialisationName = name,
                        IsActive = true
                    });
                }
            }
        }

        await context.SaveChangesAsync();


        // 3. ANIMAL SPECIES

        var speciesData = new (string Name, string Category)[]
        {
            ("Dog", "Companion"),
            ("Cat", "Companion"),
            ("Rabbit", "Companion"),
            ("Guinea Pig", "Companion"),
            ("Hamster", "Companion"),

            ("Cattle", "Farm"),
            ("Buffalo", "Farm"),
            ("Sheep", "Farm"),
            ("Goat", "Farm"),
            ("Pig", "Farm"),

            ("Horse", "Equine"),
            ("Donkey", "Equine"),

            ("Chicken", "Poultry"),
            ("Duck", "Poultry"),
            ("Goose", "Poultry"),
            ("Turkey", "Poultry"),

            ("Parrot", "Bird"),
            ("Pigeon", "Bird")
        };

        foreach (var item in speciesData)
        {
            if (!await context.Species.AnyAsync(s =>
                s.SpeciesName == item.Name))
            {
                context.Species.Add(new Species
                {
                    SpeciesName = item.Name,
                    Category = item.Category,
                    IsActive = true
                });
            }
        }

        await context.SaveChangesAsync();


        // 4. VACCINE CATALOGUE

        var vaccineData = new[]
        {
            // DOG VACCINES

            (
                Species: "Dog",
                Name: "Canine DHPPi",
                Description: "Combination vaccine covering canine distemper, hepatitis, parvovirus and parainfluenza.",
                RecommendedAge: "Usually starts at 6-8 weeks, subject to veterinary assessment.",
                RecommendedInterval: "Initial series and boosters as prescribed by veterinarian."
            ),

            (
                Species: "Dog",
                Name: "Rabies",
                Description: "Vaccine for prevention of rabies in dogs.",
                RecommendedAge: "As per applicable vaccination guidelines.",
                RecommendedInterval: "As per vaccine label and veterinarian's recommendation."
            ),

            (
                Species: "Dog",
                Name: "Leptospirosis",
                Description: "Vaccine for protection against leptospirosis.",
                RecommendedAge: "Based on veterinary assessment and regional risk.",
                RecommendedInterval: "As per vaccine label and veterinarian's recommendation."
            ),

            // CAT VACCINES

            (
                Species: "Cat",
                Name: "Feline FVRCP",
                Description: "Combination vaccine covering feline viral rhinotracheitis, calicivirus and panleukopenia.",
                RecommendedAge: "Usually starts at 6-8 weeks, subject to veterinary assessment.",
                RecommendedInterval: "Initial series and boosters as prescribed by veterinarian."
            ),

            (
                Species: "Cat",
                Name: "Rabies",
                Description: "Vaccine for prevention of rabies in cats.",
                RecommendedAge: "As per applicable vaccination guidelines.",
                RecommendedInterval: "As per vaccine label and veterinarian's recommendation."
            ),

            (
                Species: "Cat",
                Name: "Feline Leukemia (FeLV)",
                Description: "Vaccine for prevention of feline leukemia virus infection.",
                RecommendedAge: "Based on veterinary assessment and risk.",
                RecommendedInterval: "As per vaccine label and veterinarian's recommendation."
            )
        };

        foreach (var item in vaccineData)
        {
            var species = await context.Species
                .FirstAsync(s => s.SpeciesName == item.Species);

            if (!await context.Vaccines.AnyAsync(v =>
                v.SpeciesId == species.SpeciesId &&
                v.VaccineName == item.Name))
            {
                context.Vaccines.Add(new Vaccine
                {
                    VaccineName = item.Name,
                    SpeciesId = species.SpeciesId,
                    Description = item.Description,
                    RecommendedAge = item.RecommendedAge,
                    RecommendedInterval = item.RecommendedInterval,
                    IsActive = true
                });
            }
        }

        await context.SaveChangesAsync();


        // 4A. RESEARCHED VACCINE CATALOGUE
        await VaccineCatalogueSeeder.SeedAsync(context);
        await AdditionalVaccineCatalogueSeeder.SeedAsync(context);
            await VaccinationScheduleTemplateSeeder.SeedAsync(context);


        // 5. ANIMAL BREEDS

        var breedData = new Dictionary<string, string[]>
        {
            ["Dog"] =
            [
                "Labrador Retriever",
                "Golden Retriever",
                "German Shepherd",
                "Beagle",
                "Pug",
                "Pomeranian",
                "Shih Tzu",
                "Rottweiler",
                "Siberian Husky",
                "Dachshund",
                "Boxer",
                "Doberman",
                "Great Dane",
                "Indian Pariah Dog",
                "Rajapalayam",
                "Mudhol Hound",
                "Chippiparai",
                "Kombai"
            ],

            ["Cat"] =
            [
                "Persian",
                "Siamese",
                "Maine Coon",
                "Bengal",
                "British Shorthair",
                "Ragdoll",
                "Indian Domestic Shorthair",
                "Himalayan"
            ],

            ["Rabbit"] =
            [
                "Dutch",
                "Netherland Dwarf",
                "Lionhead",
                "Angora",
                "Rex",
                "New Zealand White"
            ],

            ["Guinea Pig"] =
            [
                "American",
                "Abyssinian",
                "Peruvian",
                "Silkie"
            ],

            ["Hamster"] =
            [
                "Syrian",
                "Dwarf Campbell",
                "Winter White",
                "Roborovski"
            ],

            ["Cattle"] =
            [
                "Gir",
                "Sahiwal",
                "Red Sindhi",
                "Tharparkar",
                "Ongole",
                "Hallikar",
                "Kangayam",
                "Jersey",
                "Holstein Friesian"
            ],

            ["Buffalo"] =
            [
                "Murrah",
                "Jaffarabadi",
                "Surti",
                "Mehsana",
                "Nili Ravi"
            ],

            ["Sheep"] =
            [
                "Deccani",
                "Nellore",
                "Mandya",
                "Marwari",
                "Mufflon"
            ],

            ["Goat"] =
            [
                "Jamunapari",
                "Beetal",
                "Barbari",
                "Sirohi",
                "Black Bengal",
                "Osmanabadi"
            ],

            ["Pig"] =
            [
                "Large White Yorkshire",
                "Landrace",
                "Duroc",
                "Hampshire"
            ],

            ["Horse"] =
            [
                "Marwari",
                "Kathiawari",
                "Thoroughbred",
                "Arabian",
                "Quarter Horse"
            ],

            ["Donkey"] =
            [
                "Indian Donkey",
                "Mammoth Donkey"
            ],

            ["Chicken"] =
            [
                "Aseel",
                "Kadaknath",
                "Rhode Island Red",
                "White Leghorn",
                "Plymouth Rock"
            ],

            ["Duck"] =
            [
                "Indian Runner",
                "Khaki Campbell",
                "Pekin"
            ],

            ["Goose"] =
            [
                "Embden",
                "Toulouse",
                "Chinese Goose"
            ],

            ["Turkey"] =
            [
                "Broad Breasted White",
                "Bronze Turkey"
            ],

            ["Parrot"] =
            [
                "Indian Ringneck",
                "African Grey",
                "Cockatiel",
                "Budgerigar",
                "Sun Conure"
            ],

            ["Pigeon"] =
            [
                "Fantail",
                "King Pigeon",
                "Homing Pigeon",
                "Jacobin"
            ]
        };

        foreach (var entry in breedData)
        {
            var species = await context.Species
                .FirstAsync(s => s.SpeciesName == entry.Key);

            foreach (var breedName in entry.Value)
            {
                if (!await context.Breeds.AnyAsync(b =>
                    b.SpeciesId == species.SpeciesId &&
                    b.BreedName == breedName))
                {
                    context.Breeds.Add(new Breed
                    {
                        SpeciesId = species.SpeciesId,
                        BreedName = breedName,
                        IsActive = true
                    });
                }
            }
        }

        await context.SaveChangesAsync();


        // 6. HOSPITAL SERVICES

        var services = new (string Name, string Description, decimal Price)[]
        {
            ("General Consultation",
                "Routine veterinary consultation", 400m),

            ("Specialist Consultation",
                "Consultation with a veterinary specialist", 700m),

            ("Health Check-up",
                "General physical health examination", 500m),

            ("Vaccination",
                "Administration of recommended vaccines", 350m),

            ("Deworming",
                "Veterinary deworming service", 200m),

            ("Blood Test",
                "Laboratory blood examination", 800m),

            ("X-Ray",
                "Veterinary radiography service", 1000m),

            ("Ultrasound",
                "Veterinary ultrasound examination", 1200m),

            ("Dental Cleaning",
                "Veterinary dental cleaning procedure", 1500m),

            ("Wound Dressing",
                "Cleaning and dressing of wounds", 500m),

            ("Minor Surgery",
                "Minor veterinary surgical procedure", 3000m),

            ("Major Surgery",
                "Major veterinary surgical procedure", 8000m),

            ("Emergency Treatment",
                "Treatment for emergency walk-in cases", 1500m),

            ("Pregnancy Examination",
                "Veterinary pregnancy assessment", 800m),

            ("Follow-up Consultation",
                "Follow-up veterinary consultation", 300m)
        };

        foreach (var item in services)
        {
            if (!await context.Services.AnyAsync(s =>
                s.ServiceName == item.Name))
            {
                context.Services.Add(new Service
                {
                    ServiceName = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    IsActive = true
                });
            }
        }

        await context.SaveChangesAsync();
    }
}


