using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPetEstimatedAgeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "AgeEstimateRecordedAt",
                table: "Pets",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedAgeMonths",
                table: "Pets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgeEstimated",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeEstimateRecordedAt",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "EstimatedAgeMonths",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsAgeEstimated",
                table: "Pets");
        }
    }
}
