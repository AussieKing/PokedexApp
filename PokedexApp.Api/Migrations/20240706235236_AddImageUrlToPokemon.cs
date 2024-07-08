using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokedexApp.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToPokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pokemons");
        }
    }
}
