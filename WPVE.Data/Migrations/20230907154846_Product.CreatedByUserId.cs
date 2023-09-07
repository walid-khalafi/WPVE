using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPVE.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductCreatedByUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserID",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "Products");
        }
    }
}
