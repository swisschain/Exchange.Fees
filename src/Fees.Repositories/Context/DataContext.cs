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

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .HasIndex(x => new { x.BrokerId, x.Asset }).IsUnique();

            modelBuilder.Entity<CashOperationsFeeEntity>()
                .HasIndex(x => x.Asset);

            modelBuilder.Entity<TradingFeeEntity>()
                .HasIndex(x => new { x.BrokerId, x.AssetPair }).IsUnique();

            modelBuilder.Entity<TradingFeeEntity>()
                .HasIndex(x => x.BrokerId);

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .HasIndex(x => x.Volume);

            modelBuilder.Entity<TradingFeeLevelEntity>()
                .HasOne<TradingFeeEntity>()
                .WithMany(x => x.Levels)
                .HasForeignKey(x => x.TradingFeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .HasIndex(x => x.CashOperationsFeeId);

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .HasIndex(x => x.BrokerId);

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .HasIndex(x => x.UserId);

            modelBuilder.Entity<CashOperationsFeeHistoryEntity>()
                .HasIndex(x => x.Asset);

            modelBuilder.Entity<SettingsEntity>()
                .HasIndex(x => new { x.BrokerId }).IsUnique();
        }
    }
}
