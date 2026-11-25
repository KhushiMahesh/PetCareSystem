using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePetVaccinationScheduleIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PetVaccinationSchedules_PetId",
                table: "PetVaccinationSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_PetVaccinationSchedules_PetId_VaccinationScheduleTemplateId",
                table: "PetVaccinationSchedules",
                columns: new[] { "PetId", "VaccinationScheduleTemplateId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PetVaccinationSchedules_PetId_VaccinationScheduleTemplateId",
                table: "PetVaccinationSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_PetVaccinationSchedules_PetId",
                table: "PetVaccinationSchedules",
                column: "PetId");
        }
    }
}
