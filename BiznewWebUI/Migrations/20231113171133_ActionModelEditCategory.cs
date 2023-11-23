using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiznewWebUI.Migrations
{
    /// <inheritdoc />
    public partial class ActionModelEditCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Categories_CategoryNameId",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "CategoryNameId",
                table: "Actions",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_CategoryNameId",
                table: "Actions",
                newName: "IX_Actions_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Categories_CategoryId",
                table: "Actions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Categories_CategoryId",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Actions",
                newName: "CategoryNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_CategoryId",
                table: "Actions",
                newName: "IX_Actions_CategoryNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Categories_CategoryNameId",
                table: "Actions",
                column: "CategoryNameId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
