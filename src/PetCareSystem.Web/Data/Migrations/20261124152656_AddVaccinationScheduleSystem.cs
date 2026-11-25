using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetCareSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVaccinationScheduleSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetVaccinationScheduleId",
                table: "VaccinationRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VaccinationScheduleTemplates",
                columns: table => new
                {
                    VaccinationScheduleTemplateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false),
                    VaccineId = table.Column<int>(type: "integer", nullable: false),
                    DoseName = table.Column<string>(type: "text", nullable: false),
                    DoseNumber = table.Column<int>(type: "integer", nullable: false),
                    RecommendedAgeMonths = table.Column<int>(type: "integer", nullable: false),
                    MinimumAgeMonths = table.Column<int>(type: "integer", nullable: true),
                    MaximumAgeMonths = table.Column<int>(type: "integer", nullable: true),
                    IntervalAfterPreviousDoseMonths = table.Column<int>(type: "integer", nullable: true),
                    RegionCode = table.Column<string>(type: "text", nullable: false),
                    SourceName = table.Column<string>(type: "text", nullable: true),
                    SourceReference = table.Column<string>(type: "text", nullable: true),
                    SourceReviewedOn = table.Column<DateOnly>(type: "date", nullable: true),
                    RequiresVeterinarianApproval = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationScheduleTemplates", x => x.VaccinationScheduleTemplateId);
                    table.ForeignKey(
                        name: "FK_VaccinationScheduleTemplates_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "SpeciesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaccinationScheduleTemplates_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "VaccineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PetVaccinationSchedules",
                columns: table => new
                {
                    PetVaccinationScheduleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PetId = table.Column<int>(type: "integer", nullable: false),
                    VaccinationScheduleTemplateId = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetVaccinationSchedules", x => x.PetVaccinationScheduleId);
                    table.ForeignKey(
                        name: "FK_PetVaccinationSchedules_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PetVaccinationSchedules_VaccinationScheduleTemplates_Vaccin~",
                        column: x => x.VaccinationScheduleTemplateId,
                        principalTable: "VaccinationScheduleTemplates",
                        principalColumn: "VaccinationScheduleTemplateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationRecords_PetVaccinationScheduleId",
                table: "VaccinationRecords",
                column: "PetVaccinationScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PetVaccinationSchedules_PetId",
                table: "PetVaccinationSchedules",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetVaccinationSchedules_VaccinationScheduleTemplateId",
                table: "PetVaccinationSchedules",
                column: "VaccinationScheduleTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationScheduleTemplates_SpeciesId",
                table: "VaccinationScheduleTemplates",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationScheduleTemplates_VaccineId",
                table: "VaccinationScheduleTemplates",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationRecords_PetVaccinationSchedules_PetVaccinationSc~",
                table: "VaccinationRecords",
                column: "PetVaccinationScheduleId",
                principalTable: "PetVaccinationSchedules",
                principalColumn: "PetVaccinationScheduleId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationRecords_PetVaccinationSchedules_PetVaccinationSc~",
                table: "VaccinationRecords");

            migrationBuilder.DropTable(
                name: "PetVaccinationSchedules");

            migrationBuilder.DropTable(
                name: "VaccinationScheduleTemplates");

            migrationBuilder.DropIndex(
                name: "IX_VaccinationRecords_PetVaccinationScheduleId",
                table: "VaccinationRecords");

            migrationBuilder.DropColumn(
                name: "PetVaccinationScheduleId",
                table: "VaccinationRecords");
        }
    }
}
