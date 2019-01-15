using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApplication.Core.Data.Migrations
{
    public partial class UpdatedMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Movies",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "MovieName",
                table: "Movies",
                newName: "Poster");

            migrationBuilder.AddColumn<string>(
                name: "ImdbID",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbID",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Movies",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "Poster",
                table: "Movies",
                newName: "MovieName");
        }
    }
}
