using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiznewWebUI.Migrations
{
    /// <inheritdoc />
    public partial class ActionNameReverseUserActions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Advorts_AdvortId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Articles_ArticleId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Categories_CategoryId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Tags_TagId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_userId",
                table: "Actions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actions",
                table: "Actions");

            migrationBuilder.RenameTable(
                name: "Actions",
                newName: "UserActions");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_userId",
                table: "UserActions",
                newName: "IX_UserActions_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_TagId",
                table: "UserActions",
                newName: "IX_UserActions_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_CategoryId",
                table: "UserActions",
                newName: "IX_UserActions_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_ArticleId",
                table: "UserActions",
                newName: "IX_UserActions_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_AdvortId",
                table: "UserActions",
                newName: "IX_UserActions_AdvortId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActions",
                table: "UserActions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Advorts_AdvortId",
                table: "UserActions",
                column: "AdvortId",
                principalTable: "Advorts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Articles_ArticleId",
                table: "UserActions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Categories_CategoryId",
                table: "UserActions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Tags_TagId",
                table: "UserActions",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Users_userId",
                table: "UserActions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Advorts_AdvortId",
                table: "UserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Articles_ArticleId",
                table: "UserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Categories_CategoryId",
                table: "UserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Tags_TagId",
                table: "UserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Users_userId",
                table: "UserActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActions",
                table: "UserActions");

            migrationBuilder.RenameTable(
                name: "UserActions",
                newName: "Actions");

            migrationBuilder.RenameIndex(
                name: "IX_UserActions_userId",
                table: "Actions",
                newName: "IX_Actions_userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActions_TagId",
                table: "Actions",
                newName: "IX_Actions_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActions_CategoryId",
                table: "Actions",
                newName: "IX_Actions_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActions_ArticleId",
                table: "Actions",
                newName: "IX_Actions_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActions_AdvortId",
                table: "Actions",
                newName: "IX_Actions_AdvortId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actions",
                table: "Actions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Advorts_AdvortId",
                table: "Actions",
                column: "AdvortId",
                principalTable: "Advorts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Articles_ArticleId",
                table: "Actions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Categories_CategoryId",
                table: "Actions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Tags_TagId",
                table: "Actions",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_userId",
                table: "Actions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
