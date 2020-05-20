using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerId = table.Column<string>(maxLength: 36, nullable: false),
                    Asset = table.Column<string>(maxLength: 8, nullable: false),
                    CashInValue = table.Column<decimal>(nullable: false),
                    CashInFeeType = table.Column<string>(maxLength: 16, nullable: false),
                    CashOutValue = table.Column<decimal>(nullable: false),
                    CashOutFeeType = table.Column<string>(maxLength: 16, nullable: false),
                    CashTransferValue = table.Column<decimal>(nullable: false),
                    CashTransferFeeType = table.Column<string>(maxLength: 16, nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Modified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_operations_fee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cash_operations_fee_history",
                schema: "fees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashOperationsFeeId = table.Column<long>(nullable: false),
                    BrokerId = table.Column<string>(maxLength: 36, nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: false),
                    Asset = table.Column<string>(maxLength: 8, nullable: false),
                    CashInValue = table.Column<decimal>(nullable: false),
                    CashInFeeType = table.Column<string>(maxLength: 16, nullable: false),
                    CashOutValue = table.Column<decimal>(nullable: false),
                    CashOutFeeType = table.Column<string>(maxLength: 16, nullable: false),
                    CashTransferValue = table.Column<decimal>(nullable: false),
                    CashTransferFeeType = table.Column<string>(maxLength: 16, nullable: false),
                    OperationType = table.Column<string>(maxLength: 16, nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_operations_fee_history", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                schema: "fees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerId = table.Column<string>(maxLength: 36, nullable: false),
                    FeeWalletId = table.Column<long>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Modified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trading_fee",
                schema: "fees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerId = table.Column<string>(maxLength: 36, nullable: false),
                    AssetPair = table.Column<string>(maxLength: 16, nullable: true),
                    Asset = table.Column<string>(maxLength: 8, nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Modified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trading_fee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trading_fee_level",
                schema: "fees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TradingFeeId = table.Column<long>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false),
                    MakerFee = table.Column<decimal>(nullable: false),
                    TakerFee = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Modified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trading_fee_level", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trading_fee_level_trading_fee_TradingFeeId",
                        column: x => x.TradingFeeId,
                        principalSchema: "fees",
                        principalTable: "trading_fee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_Asset",
                schema: "fees",
                table: "cash_operations_fee",
                column: "Asset");

            migrationBuilder.CreateIndex(
                name: "IX_cash_operations_fee_BrokerId_Asset",
                schema: "fees",
                table: "cash_operations_fee",
                columns: new[] { "BrokerId", "Asset" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_settings_BrokerId",
                schema: "fees",
                table: "settings",
                column: "BrokerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_BrokerId",
                schema: "fees",
                table: "trading_fee",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_BrokerId_AssetPair",
                schema: "fees",
                table: "trading_fee",
                columns: new[] { "BrokerId", "AssetPair" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_level_TradingFeeId",
                schema: "fees",
                table: "trading_fee_level",
                column: "TradingFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_trading_fee_level_Volume",
                schema: "fees",
                table: "trading_fee_level",
                column: "Volume",
                unique: true);
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
                name: "settings",
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
