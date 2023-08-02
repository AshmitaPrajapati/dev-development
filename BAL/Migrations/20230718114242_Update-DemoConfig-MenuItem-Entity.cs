using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDemoConfigMenuItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "DemoConfigEntities");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "DemoConfigEntities",
                newName: "Configuration");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "MenuItemEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "MenuItemEntities");

            migrationBuilder.RenameColumn(
                name: "Configuration",
                table: "DemoConfigEntities",
                newName: "key");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "DemoConfigEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
