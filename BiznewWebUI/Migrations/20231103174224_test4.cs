using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiznewWebUI.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Advorts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Tags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Advorts",
                type: "datetime2",
                nullable: true);
        }
    }
}
