using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dotnet.Saga.Stock.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentValue = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_product", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_stock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_stock_tb_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tb_product",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_stock_history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_stock_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_stock_history_tb_stock_StockId",
                        column: x => x.StockId,
                        principalTable: "tb_stock",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "tb_product",
                columns: new[] { "Id", "CreatedAt", "CurrentValue", "DeletedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20db60a4-c53d-4afd-a1ad-42d15f470e2c"), new DateTime(2024, 2, 27, 12, 29, 32, 5, DateTimeKind.Utc).AddTicks(9327), 3999m, null, null, false, "XBOX Series X", null },
                    { new Guid("5f36da7e-7c77-4ea5-b65f-1bf3744fdc78"), new DateTime(2024, 2, 27, 12, 29, 32, 5, DateTimeKind.Utc).AddTicks(9331), 2199m, null, null, false, "XBOX Series S", null },
                    { new Guid("b584159e-eeac-4586-bd76-557249d61daf"), new DateTime(2024, 2, 27, 12, 29, 32, 5, DateTimeKind.Utc).AddTicks(9295), 4459m, null, null, false, "PS5", null }
                });

            migrationBuilder.InsertData(
                table: "tb_stock",
                columns: new[] { "Id", "Amount", "CreatedAt", "DeletedAt", "IsDeleted", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0e45f9f6-738b-433c-a9b2-2350b38b5322"), 30, new DateTime(2024, 2, 27, 12, 29, 32, 5, DateTimeKind.Utc).AddTicks(9445), null, false, new Guid("b584159e-eeac-4586-bd76-557249d61daf"), null },
                    { new Guid("33c33659-0b4b-4667-9051-62382c64ceab"), 30, new DateTime(2024, 2, 27, 12, 29, 32, 5, DateTimeKind.Utc).AddTicks(9450), null, false, new Guid("20db60a4-c53d-4afd-a1ad-42d15f470e2c"), null },
                    { new Guid("4fa4615d-40d1-42f7-b539-4d2f74adfa0d"), 30, new DateTime(2024, 2, 27, 12, 29, 32, 5, DateTimeKind.Utc).AddTicks(9452), null, false, new Guid("5f36da7e-7c77-4ea5-b65f-1bf3744fdc78"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_ProductId",
                table: "tb_stock",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_history_StockId",
                table: "tb_stock_history",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_stock_history");

            migrationBuilder.DropTable(
                name: "tb_stock");

            migrationBuilder.DropTable(
                name: "tb_product");
        }
    }
}
