using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPVE.Data.Migrations
{
    /// <inheritdoc />
    public partial class edit_tag_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Naame",
                table: "ProductTags",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductTags",
                newName: "Naame");
        }
    }
}
