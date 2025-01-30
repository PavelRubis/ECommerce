using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RestrictDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_AccountId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("4973ddd9-bd2b-44cf-a2fc-59e6263447ec"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("b695a7b6-5322-453e-b523-f72bad68c94b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("25693cbd-69a2-46be-b5a0-3041846cc5df"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("455f706d-c597-48fd-95f9-45b909e3b5ec"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("768e1be4-4325-4470-b3ca-77c480c89bc7"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8c494754-817a-4039-aedb-0472c08745c2"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("fb5925a9-67ef-424c-b60c-98ac36924f6a"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6fdb4232-5066-446d-9879-d9e9ca3a53cc"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("9f17b751-ddcc-4377-952a-1c8e4f3639f3"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "IsDeleted", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("0ba7fd71-3a71-4f23-985e-17a8a85708a5"), false, "$2a$11$ZYbIwwDEeH3Nf.NdMwQWYOG2UHO3wavw7GLbEvWEA5mICRJ0kGKVy", "Customer", "user" },
                    { new Guid("e51c0b8a-93b1-45f3-aac9-644b7b2b3e4b"), false, "$2a$11$vn/1OQp.DXZxFcjnIZx0MupXS3UIGmrvi9P87hUs1wAjogC.yfyIK", "Manager", "admin" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Code", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("5223ede0-681a-4245-b38a-c2499ef32a3b"), "Hat", "23-3333-YY44", false, "Шляпа 'как-раз'", 7777m },
                    { new Guid("5b0fbac3-b208-487d-bef3-f231a9b9ffb7"), "Shoes", "21-3333-YY44", false, "Туфли", 8500m },
                    { new Guid("bd7c3fba-4c1b-4230-9e46-752d8f955b10"), "Hat", "22-3333-YY44", false, "Кепка", 1000m },
                    { new Guid("c1c59410-aea8-4ad7-a17f-33f2f3aef6cf"), "Jeans", "24-3333-YY44", false, "Джинсы", 2599.99m },
                    { new Guid("ede589d5-3e28-4dae-9623-a69a91a6af39"), "Dress", "20-3333-YY44", false, "Платье", 10000m }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AccountId", "Address", "Code", "Discount", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("071d0281-9149-4e9f-a450-dc983ca32e59"), new Guid("0ba7fd71-3a71-4f23-985e-17a8a85708a5"), "", "0000-2025", 10m, false, "user" },
                    { new Guid("8c54aa80-1973-4273-ac26-c91a64687346"), new Guid("e51c0b8a-93b1-45f3-aac9-644b7b2b3e4b"), "", "0000-2024", 50m, false, "admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_AccountId",
                table: "Customers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_AccountId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("071d0281-9149-4e9f-a450-dc983ca32e59"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("8c54aa80-1973-4273-ac26-c91a64687346"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5223ede0-681a-4245-b38a-c2499ef32a3b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5b0fbac3-b208-487d-bef3-f231a9b9ffb7"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("bd7c3fba-4c1b-4230-9e46-752d8f955b10"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("c1c59410-aea8-4ad7-a17f-33f2f3aef6cf"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("ede589d5-3e28-4dae-9623-a69a91a6af39"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0ba7fd71-3a71-4f23-985e-17a8a85708a5"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("e51c0b8a-93b1-45f3-aac9-644b7b2b3e4b"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_AccountId",
                table: "Customers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
