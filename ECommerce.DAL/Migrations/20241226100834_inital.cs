using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.DAL.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Category = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: true),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShipmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemsCount = table.Column<int>(type: "integer", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("6fdb4232-5066-446d-9879-d9e9ca3a53cc"), "$2a$11$i2d9t6FHgYefHUv6mvUY6ulzeDTchq/Ut5dPAJ/qFjJPH0sFIPuBa", "Manager", "admin" },
                    { new Guid("9f17b751-ddcc-4377-952a-1c8e4f3639f3"), "$2a$11$jvpIppTM1j3viV2a8R2.3ufjxM4mLQJRKD5dx3J7HGXBXoItMJLPy", "Customer", "user" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Code", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("25693cbd-69a2-46be-b5a0-3041846cc5df"), "Dress", "20-3333-YY44", "Платье", 10000m },
                    { new Guid("455f706d-c597-48fd-95f9-45b909e3b5ec"), "Hat", "22-3333-YY44", "Кепка", 1000m },
                    { new Guid("768e1be4-4325-4470-b3ca-77c480c89bc7"), "Shoes", "21-3333-YY44", "Туфли", 8500m },
                    { new Guid("8c494754-817a-4039-aedb-0472c08745c2"), "Hat", "23-3333-YY44", "Шляпа 'как-раз'", 7777m },
                    { new Guid("fb5925a9-67ef-424c-b60c-98ac36924f6a"), "Jeans", "24-3333-YY44", "Джинсы", 2599.99m }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AccountId", "Address", "Code", "Discount", "Name" },
                values: new object[,]
                {
                    { new Guid("4973ddd9-bd2b-44cf-a2fc-59e6263447ec"), new Guid("6fdb4232-5066-446d-9879-d9e9ca3a53cc"), "", "0000-2024", 50m, "admin" },
                    { new Guid("b695a7b6-5322-453e-b523-f72bad68c94b"), new Guid("9f17b751-ddcc-4377-952a-1c8e4f3639f3"), "", "0000-2025", 10m, "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AccountId",
                table: "Customers",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
