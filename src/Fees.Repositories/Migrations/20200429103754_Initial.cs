using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fees.Repositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fees");

            migrationBuilder.CreateTable(
                name: "cash_operations_fee",
                schema: "fees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    broker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    asset = table.Column<string>(type: "varchar(8)", nullable: false),
                    cash_in_value = table.Column<decimal>(type: "decimal(48,16)", nullable: false),
                    cash_in_type = table.Column<string>(type: "varchar(16)", nullable: false),
                    cash_out_value = table.Column<decimal>(type: "decimal(48,16)", nullable: false),
                    cash_out_type = table.Column<string>(type: "varchar(16)", nullable: false),
                    cash_transfer_value = table.Column<decimal>(type: "decimal(48,16)", nullable: false),
                    cash_transfer_type = table.Column<string>(type: "varchar(16)", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_operations_fee", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_asset",
                schema: "fees",
                table: "cash_operations_fee",
                column: "asset");

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_broker_id_asset",
                schema: "fees",
                table: "cash_operations_fee",
                columns: new[] { "broker_id", "asset" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_operations_fee",
                schema: "fees");
        }
    }
}
