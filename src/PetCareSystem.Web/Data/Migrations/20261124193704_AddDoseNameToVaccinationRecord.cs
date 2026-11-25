using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDoseNameToVaccinationRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoseName",
                table: "VaccinationRecords",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseName",
                table: "VaccinationRecords");
        }
    }
}
