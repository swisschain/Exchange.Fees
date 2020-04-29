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
    [Migration("20200429103754_Initial")]
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

                    b.Property<Guid>("BrokerId")
                        .HasColumnName("broker_id")
                        .HasColumnType("uuid");

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
#pragma warning restore 612, 618
        }
    }
}
