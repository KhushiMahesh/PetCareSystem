using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Data;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Services;

public class VaccinationScheduleService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<VaccinationScheduleService> _logger;

    public VaccinationScheduleService(
        ApplicationDbContext dbContext,
        ILogger<VaccinationScheduleService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // Kept for compatibility with existing calls.
    public Task<int> GenerateForPetAsync(
        int petId,
        CancellationToken cancellationToken = default)
    {
        return SynchronizeForPetAsync(petId, cancellationToken);
    }

    public async Task<int> SynchronizeForPetAsync(
        int petId,
        CancellationToken cancellationToken = default)
    {
        var pet = await _dbContext.Pets
            .AsNoTracking()
            .FirstOrDefaultAsync(
                p => p.PetId == petId,
                cancellationToken);

        if (pet == null)
        {
            throw new InvalidOperationException(
                $"Pet with ID {petId} was not found.");
        }

        DateOnly? birthDate = null;

        if (pet.DateOfBirth.HasValue && !pet.IsAgeEstimated)
        {
            birthDate = pet.DateOfBirth.Value;
        }
        else if (
            pet.IsAgeEstimated &&
            pet.EstimatedAgeMonths.HasValue &&
            pet.AgeEstimateRecordedAt.HasValue)
        {
            birthDate = pet.AgeEstimateRecordedAt.Value
                .AddMonths(-pet.EstimatedAgeMonths.Value);
        }

        // Load all existing schedules, including inactive ones.
        // Completed records are never modified.
        var existingSchedules = await _dbContext.PetVaccinationSchedules
            .Include(s => s.VaccinationRecord)
            .Where(s => s.PetId == petId)
            .ToListAsync(cancellationToken);

        if (!birthDate.HasValue)
        {
            foreach (var schedule in existingSchedules)
            {
                if (schedule.VaccinationRecord == null)
                    schedule.IsActive = false;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Schedules paused for pet {PetId}: age is unknown.",
                petId);

            return 0;
        }

        // Only active, veterinarian-approved Indian templates
        // matching the pet's species are eligible.
        var templates = await _dbContext.VaccinationScheduleTemplates
            .AsNoTracking()
            .Include(t => t.Vaccine)
            .Where(t =>
                t.SpeciesId == pet.SpeciesId &&
                t.Vaccine.SpeciesId == pet.SpeciesId &&
                t.Vaccine.IsActive &&
                t.Vaccine.ReviewStatus == "Approved" &&
                t.RegionCode == "IN" &&
                t.IsActive &&
                t.IsApproved &&
                t.ApprovedByVeterinarianProfileId != null &&
                t.ApprovedAt != null)
            .OrderBy(t => t.RecommendedAgeMonths)
            .ThenBy(t => t.DoseNumber)
            .ToListAsync(cancellationToken);

        var eligibleTemplates = templates
            .Where(t =>
                t.RecommendedAgeMonths >= 0 &&
                (!t.MinimumAgeMonths.HasValue ||
                 t.RecommendedAgeMonths >= t.MinimumAgeMonths.Value) &&
                (!t.MaximumAgeMonths.HasValue ||
                 t.RecommendedAgeMonths <= t.MaximumAgeMonths.Value))
            .ToDictionary(t => t.VaccinationScheduleTemplateId);

        var existingByTemplate = existingSchedules
            .GroupBy(s => s.VaccinationScheduleTemplateId)
            .ToDictionary(g => g.Key, g => g.First());

        var createdCount = 0;

        // Reconcile existing schedules.
        foreach (var schedule in existingSchedules)
        {
            // Never alter a schedule linked to an administered vaccine.
            if (schedule.VaccinationRecord != null)
                continue;

            if (!eligibleTemplates.TryGetValue(
                schedule.VaccinationScheduleTemplateId,
                out var template))
            {
                // The template is no longer approved, active,
                // or appropriate for this pet's species.
                schedule.IsActive = false;
                continue;
            }

            schedule.DueDate = (template.RecommendedAgeDays.HasValue ? birthDate.Value.AddDays(template.RecommendedAgeDays.Value) : birthDate.Value.AddMonths(template.RecommendedAgeMonths));

            schedule.IsActive = true;

            schedule.Notes = schedule.DueDate <
                DateOnly.FromDateTime(DateTime.UtcNow.AddHours(5.5))
                ? "Past recommended age. Veterinary catch-up assessment is required before vaccination."
                : null;
        }

        // Create schedules for newly eligible templates.
        foreach (var template in templates)
        {
            if (!eligibleTemplates.ContainsKey(
                template.VaccinationScheduleTemplateId))
            {
                continue;
            }

            if (existingByTemplate.ContainsKey(
                template.VaccinationScheduleTemplateId))
            {
                continue;
            }

            var dueDate = (template.RecommendedAgeDays.HasValue ? birthDate.Value.AddDays(template.RecommendedAgeDays.Value) : birthDate.Value.AddMonths(template.RecommendedAgeMonths));

            var today = DateOnly.FromDateTime(
                DateTime.UtcNow.AddHours(5.5));

            _dbContext.PetVaccinationSchedules.Add(
                new PetVaccinationSchedule
                {
                    PetId = petId,
                    VaccinationScheduleTemplateId =
                        template.VaccinationScheduleTemplateId,
                    DueDate = dueDate,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Notes = dueDate < today
                        ? "Past recommended age. Veterinary catch-up assessment is required before vaccination."
                        : null
                });

            createdCount++;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Synchronized vaccination schedules for pet {PetId}. Created {ScheduleCount} new schedules.",
            petId,
            createdCount);

        return createdCount;
    }
}


