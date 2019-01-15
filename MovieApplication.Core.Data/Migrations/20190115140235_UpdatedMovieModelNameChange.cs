using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApplication.Core.Data.Migrations
{
    public partial class UpdatedMovieModelNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImdbID",
                table: "Movies",
                newName: "ImdbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImdbId",
                table: "Movies",
                newName: "ImdbID");
        }
    }
}
