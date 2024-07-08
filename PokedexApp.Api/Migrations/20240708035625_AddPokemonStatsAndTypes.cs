using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokedexApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonStatsAndTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseStat = table.Column<int>(type: "int", nullable: false),
                    Effort = table.Column<int>(type: "int", nullable: false),
                    StatId = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStats_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonStats_StatInfos_StatId",
                        column: x => x.StatId,
                        principalTable: "StatInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStats_PokemonId",
                table: "PokemonStats",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStats_StatId",
                table: "PokemonStats",
                column: "StatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonStats");

            migrationBuilder.DropTable(
                name: "StatInfos");
        }
    }
}
