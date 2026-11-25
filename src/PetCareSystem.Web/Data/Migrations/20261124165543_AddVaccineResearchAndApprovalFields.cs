using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVaccineResearchAndApprovalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdministrationRoute",
                table: "Vaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Vaccines",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByVeterinarianProfileId",
                table: "Vaccines",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiseasePrevented",
                table: "Vaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendedDose",
                table: "Vaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ResearchDate",
                table: "Vaccines",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewStatus",
                table: "Vaccines",
                type: "text",
                nullable: false,
                defaultValue: "Pending Approval");

            migrationBuilder.AddColumn<string>(
                name: "SourceName",
                table: "Vaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceReference",
                table: "Vaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationStatus",
                table: "Vaccines",
                type: "text",
                nullable: false,
                defaultValue: "Not Verified");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_ApprovedByVeterinarianProfileId",
                table: "Vaccines",
                column: "ApprovedByVeterinarianProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_VeterinarianProfiles_ApprovedByVeterinarianProfile~",
                table: "Vaccines",
                column: "ApprovedByVeterinarianProfileId",
                principalTable: "VeterinarianProfiles",
                principalColumn: "VeterinarianProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VeterinarianProfiles_ApprovedByVeterinarianProfile~",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_ApprovedByVeterinarianProfileId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "AdministrationRoute",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "ApprovedByVeterinarianProfileId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "DiseasePrevented",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "RecommendedDose",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "ResearchDate",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "SourceName",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "SourceReference",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "Vaccines");
        }
    }
}
