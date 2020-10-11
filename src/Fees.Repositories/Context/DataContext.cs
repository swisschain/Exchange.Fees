using Fees.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;

namespace Fees.Repositories.Context
{
    public class DataContext : DbContext
    {
        private const string Schema = "fees";

        private string _connectionString;

        private readonly ILoggerFactory _loggerFactory;

        public DataContext()
        {
        }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataContext(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _loggerFactory = loggerFactory;
        }

        internal DbSet<CashOperationsFeeEntity> CashOperationsFees { get; set; }

        internal DbSet<TradingFeeEntity> TradingFees { get; set; }

        internal DbSet<TradingFeeLevelEntity> TradingFeeLevels { get; set; }

        internal DbSet<CashOperationsFeeHistoryEntity> CashOperationsFeeHistories { get; set; }

        internal DbSet<SettingsEntity> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString == null)
            {
                System.Console.Write("Enter connection string: ");
                _connectionString = System.Console.ReadLine();
            }

            optionsBuilder.EnableDetailedErrors();

            if (_loggerFactory != null)
                optionsBuilder.UseLoggerFactory(_loggerFactory);

            optionsBuilder.UseNpgsql(_connectionString,
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            CashOperationsFeeEntity(modelBuilder);

            TradingFeeEntity(modelBuilder);

            TradingFeeLevelEntity(modelBuilder);

            SettingsEntity(modelBuilder);

            CashOperationsFeeHistoryEntity(modelBuilder);
        }

        private static void CashOperationsFeeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashOperationsFeeEntity>()
                .ToTable("cash_operations_fee")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.BrokerId)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.Asset)
                .HasMaxLength(8)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.CashInValue)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.CashInFeeType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.CashOutValue)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.CashOutFeeType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.CashTransferValue)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.CashTransferFeeType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.Created)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .Property(x => x.Modified)
                .IsRequired();


            modelBuilder.Entity<CashOperationsFeeEntity>()
                .HasIndex(x => new { x.BrokerId, x.Asset }).IsUnique();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .HasIndex(x => x.Asset);
        }

        private static void TradingFeeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TradingFeeEntity>()
                .ToTable("trading_fee")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<TradingFeeEntity>()
                .Property(x => x.BrokerId)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<TradingFeeEntity>()
                .Property(x => x.AssetPair)
                .HasMaxLength(16);

            modelBuilder.Entity<TradingFeeEntity>()
                .Property(x => x.Asset)
                .HasMaxLength(8);

            modelBuilder.Entity<TradingFeeEntity>()
                .HasMany(x => x.Levels)
                .WithOne()
                .HasForeignKey(x => x.TradingFeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<TradingFeeEntity>()
                .Property(x => x.Created)
                .IsRequired();

            modelBuilder.Entity<TradingFeeEntity>()
                .Property(x => x.Modified)
                .IsRequired();


            modelBuilder.Entity<TradingFeeEntity>()
                .HasIndex(x => x.BrokerId);

            modelBuilder.Entity<TradingFeeEntity>()
                .HasIndex(x => new { x.BrokerId, x.AssetPair }).IsUnique();
        }

        private static void TradingFeeLevelEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TradingFeeLevelEntity>()
                .ToTable("trading_fee_level")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .HasIndex(x => x.Volume)
                .IsUnique();

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .Property(x => x.TradingFeeId)
                .IsRequired();

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .Property(x => x.Volume);

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .Property(x => x.MakerFee)
                .IsRequired();

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .Property(x => x.TakerFee)
                .IsRequired();

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .Property(x => x.Created)
                .IsRequired();

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .Property(x => x.Modified)
                .IsRequired();

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .HasIndex(x => new { x.Volume }).IsUnique();
        }

        private static void SettingsEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsEntity>()
                .ToTable("settings")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<SettingsEntity>()
                .Property(x => x.BrokerId)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<SettingsEntity>()
                .Property(x => x.FeeAccountId)
                .IsRequired();

            modelBuilder.Entity<SettingsEntity>()
                .Property(x => x.FeeWalletId)
                .IsRequired();

            modelBuilder.Entity<SettingsEntity>()
                .Property(x => x.Created)
                .IsRequired();

            modelBuilder.Entity<SettingsEntity>()
                .Property(x => x.Modified)
                .IsRequired();


            modelBuilder.Entity<SettingsEntity>()
                .HasIndex(x => new { x.BrokerId })
                .IsUnique();
        }

        private static void CashOperationsFeeHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .ToTable("cash_operations_fee_history")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.CashOperationsFeeId)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.BrokerId)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.UserId)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.Asset)
                .HasMaxLength(8)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.CashInValue)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.CashInFeeType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.CashOutValue)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.CashOutFeeType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.CashTransferValue)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.CashTransferFeeType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.OperationType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .Property(x => x.Timestamp)
                .IsRequired();
        }
    }
}
