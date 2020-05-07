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
                    broker_id = table.Column<string>(type: "varchar(36)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "cash_operations_fee_history",
                schema: "fees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cash_operations_fee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    broker_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    user_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    asset = table.Column<string>(type: "varchar(8)", nullable: false),
                    cash_in_value = table.Column<decimal>(type: "decimal(48,16)", nullable: false),
                    cash_in_type = table.Column<string>(type: "varchar(16)", nullable: false),
                    cash_out_value = table.Column<decimal>(type: "decimal(48,16)", nullable: false),
                    cash_out_type = table.Column<string>(type: "varchar(16)", nullable: false),
                    cash_transfer_value = table.Column<decimal>(type: "decimal(48,16)", nullable: false),
                    cash_transfer_type = table.Column<string>(type: "varchar(16)", nullable: false),
                    history_operation_type = table.Column<string>(type: "varchar(16)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_operations_fee_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trading_fee",
                schema: "fees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    broker_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    assetPair = table.Column<string>(type: "varchar(16)", nullable: true),
                    asset = table.Column<string>(type: "varchar(8)", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trading_fee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trading_fee_level",
                schema: "fees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    trading_fee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    volume = table.Column<decimal>(type: "decimal", nullable: false),
                    maker_fee = table.Column<decimal>(type: "decimal", nullable: false),
                    taker_fee = table.Column<decimal>(type: "decimal", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trading_fee_level", x => x.id);
                    table.ForeignKey(
                        name: "FK_trading_fee_level_trading_fee_trading_fee_id",
                        column: x => x.trading_fee_id,
                        principalSchema: "fees",
                        principalTable: "trading_fee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_history_asset",
                schema: "fees",
                table: "cash_operations_fee_history",
                column: "asset");

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_history_broker_id",
                schema: "fees",
                table: "cash_operations_fee_history",
                column: "broker_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_history_cash_operations_fee_id",
                schema: "fees",
                table: "cash_operations_fee_history",
                column: "cash_operations_fee_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_history_user_id",
                schema: "fees",
                table: "cash_operations_fee_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_broker_id",
                schema: "fees",
                table: "trading_fee",
                column: "broker_id");

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_broker_id_assetPair",
                schema: "fees",
                table: "trading_fee",
                columns: new[] { "broker_id", "assetPair" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_level_trading_fee_id",
                schema: "fees",
                table: "trading_fee_level",
                column: "trading_fee_id");

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_level_volume",
                schema: "fees",
                table: "trading_fee_level",
                column: "volume");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_operations_fee",
                schema: "fees");

            migrationBuilder.DropTable(
                name: "cash_operations_fee_history",
                schema: "fees");

            migrationBuilder.DropTable(
                name: "trading_fee_level",
                schema: "fees");

            migrationBuilder.DropTable(
                name: "trading_fee",
                schema: "fees");
        }
    }
}
