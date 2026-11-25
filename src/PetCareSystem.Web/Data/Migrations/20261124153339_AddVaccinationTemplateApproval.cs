using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVaccinationTemplateApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "VaccinationScheduleTemplates",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByVeterinarianProfileId",
                table: "VaccinationScheduleTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "VaccinationScheduleTemplates",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationScheduleTemplates_ApprovedByVeterinarianProfileId",
                table: "VaccinationScheduleTemplates",
                column: "ApprovedByVeterinarianProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationScheduleTemplates_VeterinarianProfiles_ApprovedB~",
                table: "VaccinationScheduleTemplates",
                column: "ApprovedByVeterinarianProfileId",
                principalTable: "VeterinarianProfiles",
                principalColumn: "VeterinarianProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationScheduleTemplates_VeterinarianProfiles_ApprovedB~",
                table: "VaccinationScheduleTemplates");

            migrationBuilder.DropIndex(
                name: "IX_VaccinationScheduleTemplates_ApprovedByVeterinarianProfileId",
                table: "VaccinationScheduleTemplates");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "VaccinationScheduleTemplates");

            migrationBuilder.DropColumn(
                name: "ApprovedByVeterinarianProfileId",
                table: "VaccinationScheduleTemplates");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "VaccinationScheduleTemplates");
        }
    }
}
