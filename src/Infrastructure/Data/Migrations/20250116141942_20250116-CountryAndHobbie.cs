using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ca.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _20250116CountryAndHobbie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Peoples",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Peoples",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Peoples",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Countrys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countrys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HobbiePeople",
                columns: table => new
                {
                    HobbiesId = table.Column<int>(type: "int", nullable: false),
                    PeoplesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbiePeople", x => new { x.HobbiesId, x.PeoplesId });
                    table.ForeignKey(
                        name: "FK_HobbiePeople_Hobbies_HobbiesId",
                        column: x => x.HobbiesId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbiePeople_Peoples_PeoplesId",
                        column: x => x.PeoplesId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_CountryId",
                table: "Peoples",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbiePeople_PeoplesId",
                table: "HobbiePeople",
                column: "PeoplesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Countrys_CountryId",
                table: "Peoples",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Countrys_CountryId",
                table: "Peoples");

            migrationBuilder.DropTable(
                name: "Countrys");

            migrationBuilder.DropTable(
                name: "HobbiePeople");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropIndex(
                name: "IX_Peoples_CountryId",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Peoples");
        }
    }
}
