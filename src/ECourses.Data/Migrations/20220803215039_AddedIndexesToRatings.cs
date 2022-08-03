using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECourses.Data.Migrations
{
    public partial class AddedIndexesToRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourseId_UserId",
                table: "Ratings",
                columns: new[] { "CourseId", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_CourseId_UserId",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings",
                column: "CourseId");
        }
    }
}
