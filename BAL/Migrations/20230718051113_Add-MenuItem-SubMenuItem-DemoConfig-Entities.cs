using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuItemSubMenuItemDemoConfigEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItemEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubMenuItemEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubMenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenuItemEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenuItemEntities_MenuItemEntities_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItemEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemoConfigEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubMenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoConfigEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemoConfigEntities_SubMenuItemEntities_SubMenuItemId",
                        column: x => x.SubMenuItemId,
                        principalTable: "SubMenuItemEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemoConfigEntities_SubMenuItemId",
                table: "DemoConfigEntities",
                column: "SubMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenuItemEntities_MenuItemId",
                table: "SubMenuItemEntities",
                column: "MenuItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoConfigEntities");

            migrationBuilder.DropTable(
                name: "SubMenuItemEntities");

            migrationBuilder.DropTable(
                name: "MenuItemEntities");
        }
    }
}
