using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delab.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CountryStateCityDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodPhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    IdState = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryIdCountry = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.IdState);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryIdCountry",
                        column: x => x.CountryIdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdState = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateIdState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.IdCity);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateIdState",
                        column: x => x.StateIdState,
                        principalTable: "States",
                        principalColumn: "IdState",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_IdState",
                table: "Cities",
                columns: new[] { "Name", "IdState" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateIdState",
                table: "Cities",
                column: "StateIdState");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryIdCountry",
                table: "States",
                column: "CountryIdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_States_Name_IdCountry",
                table: "States",
                columns: new[] { "Name", "IdCountry" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
