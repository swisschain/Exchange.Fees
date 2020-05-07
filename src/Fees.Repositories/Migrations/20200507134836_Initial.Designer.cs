﻿// <auto-generated />
using System;
using Fees.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fees.Repositories.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200507134836_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("fees")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Fees.Repositories.DTOs.CashOperationsFeeData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Asset")
                        .IsRequired()
                        .HasColumnName("asset")
                        .HasColumnType("varchar(8)");

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnName("broker_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CashInFeeType")
                        .IsRequired()
                        .HasColumnName("cash_in_type")
                        .HasColumnType("varchar(16)");

                    b.Property<decimal>("CashInValue")
                        .HasColumnName("cash_in_value")
                        .HasColumnType("decimal(48,16)");

                    b.Property<string>("CashOutFeeType")
                        .IsRequired()
                        .HasColumnName("cash_out_type")
                        .HasColumnType("varchar(16)");

                    b.Property<decimal>("CashOutValue")
                        .HasColumnName("cash_out_value")
                        .HasColumnType("decimal(48,16)");

                    b.Property<string>("CashTransferFeeType")
                        .IsRequired()
                        .HasColumnName("cash_transfer_type")
                        .HasColumnType("varchar(16)");

                    b.Property<decimal>("CashTransferValue")
                        .HasColumnName("cash_transfer_value")
                        .HasColumnType("decimal(48,16)");

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Modified")
                        .HasColumnName("modified")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Asset");

                    b.HasIndex("BrokerId", "Asset")
                        .IsUnique();

                    b.ToTable("cash_operations_fee");
                });

            modelBuilder.Entity("Fees.Repositories.DTOs.CashOperationsFeeHistoryData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Asset")
                        .IsRequired()
                        .HasColumnName("asset")
                        .HasColumnType("varchar(8)");

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnName("broker_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CashInFeeType")
                        .IsRequired()
                        .HasColumnName("cash_in_type")
                        .HasColumnType("varchar(16)");

                    b.Property<decimal>("CashInValue")
                        .HasColumnName("cash_in_value")
                        .HasColumnType("decimal(48,16)");

                    b.Property<Guid>("CashOperationsFeeId")
                        .HasColumnName("cash_operations_fee_id")
                        .HasColumnType("uuid");

                    b.Property<string>("CashOutFeeType")
                        .IsRequired()
                        .HasColumnName("cash_out_type")
                        .HasColumnType("varchar(16)");

                    b.Property<decimal>("CashOutValue")
                        .HasColumnName("cash_out_value")
                        .HasColumnType("decimal(48,16)");

                    b.Property<string>("CashTransferFeeType")
                        .IsRequired()
                        .HasColumnName("cash_transfer_type")
                        .HasColumnType("varchar(16)");

                    b.Property<decimal>("CashTransferValue")
                        .HasColumnName("cash_transfer_value")
                        .HasColumnType("decimal(48,16)");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasColumnName("history_operation_type")
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("Asset");

                    b.HasIndex("BrokerId");

                    b.HasIndex("CashOperationsFeeId");

                    b.HasIndex("UserId");

                    b.ToTable("cash_operations_fee_history");
                });

            modelBuilder.Entity("Fees.Repositories.DTOs.TradingFeeData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Asset")
                        .HasColumnName("asset")
                        .HasColumnType("varchar(8)");

                    b.Property<string>("AssetPair")
                        .HasColumnName("assetPair")
                        .HasColumnType("varchar(16)");

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnName("broker_id")
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Modified")
                        .HasColumnName("modified")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.HasIndex("BrokerId", "AssetPair")
                        .IsUnique();

                    b.ToTable("trading_fee");
                });

            modelBuilder.Entity("Fees.Repositories.DTOs.TradingFeeLevelData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("MakerFee")
                        .HasColumnName("maker_fee")
                        .HasColumnType("decimal");

                    b.Property<DateTime>("Modified")
                        .HasColumnName("modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("TakerFee")
                        .HasColumnName("taker_fee")
                        .HasColumnType("decimal");

                    b.Property<Guid>("TradingFeeId")
                        .HasColumnName("trading_fee_id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Volume")
                        .HasColumnName("volume")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("TradingFeeId");

                    b.HasIndex("Volume");

                    b.ToTable("trading_fee_level");
                });

            modelBuilder.Entity("Fees.Repositories.DTOs.TradingFeeLevelData", b =>
                {
                    b.HasOne("Fees.Repositories.DTOs.TradingFeeData", null)
                        .WithMany()
                        .HasForeignKey("TradingFeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}