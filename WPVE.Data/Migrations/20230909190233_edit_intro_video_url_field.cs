using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPVE.Data.Migrations
{
    /// <inheritdoc />
    public partial class edit_intro_video_url_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntoVideoUrl",
                table: "Products",
                newName: "IntroVideoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntroVideoUrl",
                table: "Products",
                newName: "IntoVideoUrl");
        }
    }
}
