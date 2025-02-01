using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPSP.Services.Migrations
{
    /// <inheritdoc />
    public partial class addMood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoodTracker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DodatniOpis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumEvidencije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    MoodTrackerStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodTracker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoodTracker_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderDateTime",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9285));

            migrationBuilder.UpdateData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9681));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9505));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 2,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9512));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 3,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9515));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 4,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9518));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 5,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9521));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 6,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 7,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9527));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 8,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9531));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 9,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 12, 51, 19, 177, DateTimeKind.Local).AddTicks(9536));

            migrationBuilder.CreateIndex(
                name: "IX_MoodTracker_CustomerId",
                table: "MoodTracker",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoodTracker");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderDateTime",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(5938));

            migrationBuilder.UpdateData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6262));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6132));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 2,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6139));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 3,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6145));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 4,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6148));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 5,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6151));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 6,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6154));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 7,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6157));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 8,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "SaleInvoice",
                keyColumn: "Id",
                keyValue: 9,
                column: "SaleDate",
                value: new DateTime(2025, 1, 30, 9, 33, 42, 677, DateTimeKind.Local).AddTicks(6162));
        }
    }
}
