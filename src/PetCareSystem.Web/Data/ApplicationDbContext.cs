using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // ==========================================
    // APPLICATION TABLES
    // ==========================================

    // Owner and veterinarian profiles
    public DbSet<OwnerProfile> OwnerProfiles => Set<OwnerProfile>();
    public DbSet<VeterinarianProfile> VeterinarianProfiles => Set<VeterinarianProfile>();

    // Animal management
    public DbSet<Species> Species => Set<Species>();
    public DbSet<Breed> Breeds => Set<Breed>();
    public DbSet<Pet> Pets => Set<Pet>();

    // Hospital departments and specialisations
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Specialisation> Specialisations => Set<Specialisation>();

    // Veterinarian management
    public DbSet<VeterinarianSpecialisation> VeterinarianSpecialisations => Set<VeterinarianSpecialisation>();
    public DbSet<VeterinarianSpecies> VeterinarianSpecies => Set<VeterinarianSpecies>();
    public DbSet<VeterinarianSchedule> VeterinarianSchedules => Set<VeterinarianSchedule>();
    public DbSet<ScheduleException> ScheduleExceptions => Set<ScheduleException>();

    // Appointment management
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentStatusHistory> AppointmentStatusHistories => Set<AppointmentStatusHistory>();

    // Emergency management
    public DbSet<EmergencyCase> EmergencyCases => Set<EmergencyCase>();

    // Medical records
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();
    public DbSet<Vaccine> Vaccines => Set<Vaccine>();
    public DbSet<VaccinationRecord> VaccinationRecords => Set<VaccinationRecord>();
    public DbSet<VaccinationScheduleTemplate> VaccinationScheduleTemplates => Set<VaccinationScheduleTemplate>();
    public DbSet<PetVaccinationSchedule> PetVaccinationSchedules => Set<PetVaccinationSchedule>();

    // Billing management
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<AppointmentService> AppointmentServices => Set<AppointmentService>();


    // ==========================================
    // DATABASE RELATIONSHIPS AND CONFIGURATION
    // ==========================================

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ==========================================
        // 1. OWNER PROFILE - IDENTITY ACCOUNT
        // ==========================================

        builder.Entity<OwnerProfile>()
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<OwnerProfile>(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OwnerProfile>()
            .HasIndex(o => o.UserId)
            .IsUnique();


        // ==========================================
        // 2. VETERINARIAN PROFILE - IDENTITY ACCOUNT
        // ==========================================

        builder.Entity<VeterinarianProfile>()
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<VeterinarianProfile>(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<VeterinarianProfile>()
            .HasIndex(v => v.UserId)
            .IsUnique();


        // ==========================================
        // 3. SPECIES - BREEDS
        // ==========================================

        builder.Entity<Species>()
            .HasMany(s => s.Breeds)
            .WithOne(b => b.Species)
            .HasForeignKey(b => b.SpeciesId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 4. OWNER - PETS
        // ==========================================

        builder.Entity<Pet>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerProfileId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 5. SPECIES - PETS
        // ==========================================

        builder.Entity<Pet>()
            .HasOne(p => p.Species)
            .WithMany()
            .HasForeignKey(p => p.SpeciesId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 6. BREED - PETS
        // ==========================================

        builder.Entity<Pet>()
            .HasOne(p => p.Breed)
            .WithMany()
            .HasForeignKey(p => p.BreedId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 7. DEPARTMENT - SPECIALISATION
        // ==========================================

        builder.Entity<Specialisation>()
            .HasOne(s => s.Department)
            .WithMany(d => d.Specialisations)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 8. VETERINARIAN - SPECIALISATION
        // Many-to-many relationship
        // ==========================================

        builder.Entity<VeterinarianSpecialisation>()
            .HasKey(vs => new
            {
                vs.VeterinarianProfileId,
                vs.SpecialisationId
            });

        builder.Entity<VeterinarianSpecialisation>()
            .HasOne(vs => vs.Veterinarian)
            .WithMany()
            .HasForeignKey(vs => vs.VeterinarianProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VeterinarianSpecialisation>()
            .HasOne(vs => vs.Specialisation)
            .WithMany()
            .HasForeignKey(vs => vs.SpecialisationId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 9. VETERINARIAN - SPECIES
        // Many-to-many relationship
        // ==========================================

        builder.Entity<VeterinarianSpecies>()
            .HasKey(vs => new
            {
                vs.VeterinarianProfileId,
                vs.SpeciesId
            });

        builder.Entity<VeterinarianSpecies>()
            .HasOne(vs => vs.Veterinarian)
            .WithMany()
            .HasForeignKey(vs => vs.VeterinarianProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VeterinarianSpecies>()
            .HasOne(vs => vs.Species)
            .WithMany()
            .HasForeignKey(vs => vs.SpeciesId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 10. VETERINARIAN - SCHEDULE
        // ==========================================

        builder.Entity<VeterinarianSchedule>()
            .HasOne(s => s.Veterinarian)
            .WithMany()
            .HasForeignKey(s => s.VeterinarianProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VeterinarianSchedule>()
            .HasIndex(s => new
            {
                s.VeterinarianProfileId,
                s.DayOfWeek
            })
            .IsUnique();


        // ==========================================
        // 11. SCHEDULE EXCEPTIONS
        // ==========================================

        builder.Entity<ScheduleException>()
            .HasOne(s => s.Veterinarian)
            .WithMany()
            .HasForeignKey(s => s.VeterinarianProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ScheduleException>()
            .HasIndex(s => new
            {
                s.VeterinarianProfileId,
                s.ExceptionDate
            })
            .IsUnique();


        // ==========================================
        // 12. APPOINTMENTS
        // ==========================================

        builder.Entity<Appointment>()
            .HasOne(a => a.Owner)
            .WithMany()
            .HasForeignKey(a => a.OwnerProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Appointment>()
            .HasOne(a => a.Pet)
            .WithMany()
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Appointment>()
            .HasOne(a => a.Veterinarian)
            .WithMany()
            .HasForeignKey(a => a.VeterinarianProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Appointment>()
            .HasOne(a => a.Specialisation)
            .WithMany()
            .HasForeignKey(a => a.SpecialisationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Appointment>()
            .HasIndex(a => new
            {
                a.VeterinarianProfileId,
                a.StartDateTime,
                a.EndDateTime
            });

        builder.Entity<Appointment>()
            .ToTable(t => t.HasCheckConstraint(
                "CK_Appointments_ValidTime",
                "\"EndDateTime\" > \"StartDateTime\""));


        // ==========================================
        // 13. APPOINTMENT STATUS HISTORY
        // ==========================================

        builder.Entity<AppointmentStatusHistory>()
            .HasOne(h => h.Appointment)
            .WithMany()
            .HasForeignKey(h => h.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 14. EMERGENCY CASES
        // ==========================================

        builder.Entity<EmergencyCase>()
            .HasOne(e => e.Pet)
            .WithMany()
            .HasForeignKey(e => e.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<EmergencyCase>()
            .HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<EmergencyCase>()
            .HasOne(e => e.Veterinarian)
            .WithMany()
            .HasForeignKey(e => e.VeterinarianProfileId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 15. MEDICAL RECORDS
        // ==========================================

        builder.Entity<MedicalRecord>()
            .HasOne(m => m.Pet)
            .WithMany()
            .HasForeignKey(m => m.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<MedicalRecord>()
            .HasOne(m => m.Appointment)
            .WithMany()
            .HasForeignKey(m => m.AppointmentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<MedicalRecord>()
            .HasOne(m => m.EmergencyCase)
            .WithMany()
            .HasForeignKey(m => m.EmergencyCaseId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<MedicalRecord>()
            .HasOne(m => m.Veterinarian)
            .WithMany()
            .HasForeignKey(m => m.VeterinarianProfileId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 16. PRESCRIPTIONS
        // ==========================================

        builder.Entity<Prescription>()
            .HasOne(p => p.MedicalRecord)
            .WithMany()
            .HasForeignKey(p => p.MedicalRecordId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 17. PRESCRIPTION ITEMS
        // ==========================================

        builder.Entity<PrescriptionItem>()
            .HasOne(p => p.Prescription)
            .WithMany()
            .HasForeignKey(p => p.PrescriptionId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 18. VACCINES
        // ==========================================

        builder.Entity<Vaccine>()
            .HasOne(v => v.Species)
            .WithMany()
            .HasForeignKey(v => v.SpeciesId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Vaccine>()
            .HasOne(v => v.ApprovedByVeterinarian)
            .WithMany()
            .HasForeignKey(v => v.ApprovedByVeterinarianProfileId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 19. VACCINATION RECORDS
        // ==========================================

        builder.Entity<VaccinationRecord>()
            .HasOne(v => v.Pet)
            .WithMany()
            .HasForeignKey(v => v.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VaccinationRecord>()
            .HasOne(v => v.Vaccine)
            .WithMany()
            .HasForeignKey(v => v.VaccineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VaccinationRecord>()
            .HasOne(v => v.Veterinarian)
            .WithMany()
            .HasForeignKey(v => v.VeterinarianProfileId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        builder.Entity<VaccinationRecord>()
            .HasOne(v => v.PetVaccinationSchedule)
            .WithOne(s => s.VaccinationRecord)
            .HasForeignKey<VaccinationRecord>(v => v.PetVaccinationScheduleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================================
        // 20. VACCINATION SCHEDULE TEMPLATES
        // ==========================================

        builder.Entity<VaccinationScheduleTemplate>()
            .HasOne(t => t.Species)
            .WithMany()
            .HasForeignKey(t => t.SpeciesId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VaccinationScheduleTemplate>()
            .HasOne(t => t.Vaccine)
            .WithMany()
            .HasForeignKey(t => t.VaccineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VaccinationScheduleTemplate>()
            .HasOne(t => t.ApprovedByVeterinarian)
            .WithMany()
            .HasForeignKey(t => t.ApprovedByVeterinarianProfileId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================================
        // 21. PET VACCINATION SCHEDULES
        // ==========================================

        builder.Entity<PetVaccinationSchedule>()
            .HasOne(s => s.Pet)
            .WithMany()
            .HasForeignKey(s => s.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PetVaccinationSchedule>()
            .HasOne(s => s.Template)
            .WithMany()
            .HasForeignKey(s => s.VaccinationScheduleTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PetVaccinationSchedule>()
            .HasIndex(s => new
            {
                s.PetId,
                s.VaccinationScheduleTemplateId
            })
            .IsUnique();
        // 20. INVOICES
        // ==========================================

        builder.Entity<Invoice>()
            .HasOne(i => i.Owner)
            .WithMany()
            .HasForeignKey(i => i.OwnerProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Invoice>()
            .HasOne(i => i.Appointment)
            .WithMany()
            .HasForeignKey(i => i.AppointmentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Invoice>()
            .HasOne(i => i.EmergencyCase)
            .WithMany()
            .HasForeignKey(i => i.EmergencyCaseId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Invoice>()
            .HasIndex(i => i.InvoiceNumber)
            .IsUnique();


        // ==========================================
        // 21. INVOICE ITEMS
        // ==========================================

        builder.Entity<InvoiceItem>()
            .HasOne(i => i.Invoice)
            .WithMany()
            .HasForeignKey(i => i.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 22. PAYMENTS
        // ==========================================

        builder.Entity<Payment>()
            .HasOne(p => p.Invoice)
            .WithMany()
            .HasForeignKey(p => p.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // 23. SERVICES
        // ==========================================

        builder.Entity<Service>()
            .HasIndex(s => s.ServiceName)
            .IsUnique();


        // ==========================================
        // 24. APPOINTMENT SERVICES
        // ==========================================

        builder.Entity<AppointmentService>()
            .HasOne(a => a.Appointment)
            .WithMany()
            .HasForeignKey(a => a.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AppointmentService>()
            .HasOne(a => a.Service)
            .WithMany()
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);


        // ==========================================
        // DECIMAL PRECISION CONFIGURATION
        // ==========================================

        builder.Entity<VeterinarianProfile>()
            .Property(v => v.ConsultationFee)
            .HasPrecision(10, 2);

        builder.Entity<Pet>()
            .Property(p => p.Weight)
            .HasPrecision(8, 2);

        builder.Entity<Invoice>()
            .Property(i => i.SubTotal)
            .HasPrecision(12, 2);

        builder.Entity<Invoice>()
            .Property(i => i.TaxAmount)
            .HasPrecision(12, 2);

        builder.Entity<Invoice>()
            .Property(i => i.DiscountAmount)
            .HasPrecision(12, 2);

        builder.Entity<Invoice>()
            .Property(i => i.TotalAmount)
            .HasPrecision(12, 2);

        builder.Entity<InvoiceItem>()
            .Property(i => i.UnitPrice)
            .HasPrecision(12, 2);

        builder.Entity<InvoiceItem>()
            .Property(i => i.TotalPrice)
            .HasPrecision(12, 2);

        builder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasPrecision(12, 2);

        builder.Entity<Service>()
            .Property(s => s.Price)
            .HasPrecision(12, 2);

        builder.Entity<AppointmentService>()
            .Property(a => a.UnitPrice)
            .HasPrecision(12, 2);
    }
}