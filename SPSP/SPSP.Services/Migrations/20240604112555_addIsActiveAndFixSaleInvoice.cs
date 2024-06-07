using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPSP.Services.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveAndFixSaleInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoice_PaymentGatewayDataId",
                table: "PurchaseInvoice");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseInvoice_PaymentGatewayDataId",
                table: "PurchaseInvoice");

            migrationBuilder.DropColumn(
                name: "PaymentGatewayDataId",
                table: "PurchaseInvoice");

            migrationBuilder.AddColumn<int>(
                name: "PaymentGatewayDataId",
                table: "SaleInvoice",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 5,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 6,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 7,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 8,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 9,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 10,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 11,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 12,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 13,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 14,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 15,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(8924));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 17,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 18,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 19,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 20,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 21,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 22,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 23,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 24,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 25,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 26,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 27,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 28,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 29,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 30,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 31,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 32,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 33,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 34,
                column: "OrderDateTime",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9356));

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 6, 4, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 6, 4, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 6, 4, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 4, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 4, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 4, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 8, 6, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 6, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 8, 6, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 6, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 8, 6, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 6, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9144) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9153) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9158) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9163) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9166) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9173) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9179) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PaymentGatewayDataId", "SaleDate" },
                values: new object[] { null, new DateTime(2024, 6, 4, 13, 25, 54, 141, DateTimeKind.Local).AddTicks(9182) });

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoice_PaymentGatewayDataId",
                table: "SaleInvoice",
                column: "PaymentGatewayDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleInvoice_PaymentGatewayDataId",
                table: "SaleInvoice",
                column: "PaymentGatewayDataId",
                principalTable: "PaymentGatewayData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleInvoice_PaymentGatewayDataId",
                table: "SaleInvoice");

            migrationBuilder.DropIndex(
                name: "IX_SaleInvoice_PaymentGatewayDataId",
                table: "SaleInvoice");

            migrationBuilder.DropColumn(
                name: "PaymentGatewayDataId",
                table: "SaleInvoice");

            migrationBuilder.AddColumn<int>(
                name: "PaymentGatewayDataId",
                table: "PurchaseInvoice",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: null);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 5,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 6,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 7,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 8,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 9,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 10,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 11,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 12,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 13,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 14,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 15,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7600));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 17,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 18,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 19,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 20,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 21,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 22,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 23,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 24,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 25,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 26,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 27,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 28,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 29,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 30,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 31,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 32,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 33,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 34,
                column: "OrderDateTime",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(8012));

            migrationBuilder.UpdateData(
                table: "PurchaseInvoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentGatewayDataId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PurchaseInvoice",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentGatewayDataId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PurchaseInvoice",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentGatewayDataId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 14, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 14, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 14, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 14, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 14, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 14, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 6, 14, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 14, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 6, 14, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 14, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 6, 14, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 14, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 16, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 16, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 16, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7841));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 2,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7849));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 3,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 4,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 5,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7860));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 6,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 7,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 8,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7868));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 9,
                column: "SaleDate",
                value: new DateTime(2024, 5, 14, 22, 29, 15, 385, DateTimeKind.Local).AddTicks(7871));

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoice_PaymentGatewayDataId",
                table: "PurchaseInvoice",
                column: "PaymentGatewayDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoice_PaymentGatewayDataId",
                table: "PurchaseInvoice",
                column: "PaymentGatewayDataId",
                principalTable: "PaymentGatewayData",
                principalColumn: "Id");
        }
    }
}
