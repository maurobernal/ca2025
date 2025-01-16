using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ca.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _20250116Peopleconfigurator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Countrys_CountryId",
                table: "Peoples");

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Countrys_CountryId",
                table: "Peoples",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Countrys_CountryId",
                table: "Peoples");

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Countrys_CountryId",
                table: "Peoples",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
