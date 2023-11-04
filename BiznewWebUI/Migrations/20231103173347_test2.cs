using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiznewWebUI.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Advorts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedUserId",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUserId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUserId",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUserId",
                table: "Advorts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
