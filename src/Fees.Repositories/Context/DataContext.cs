using Fees.Repositories.DTOs;
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

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataContext(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _loggerFactory = loggerFactory;
        }

        internal DbSet<CashOperationsFeeData> CashOperationsFees { get; set; }

        internal DbSet<TradingFeeData> TradingFees { get; set; }

        internal DbSet<TradingFeeLevelData> TradingFeeLevels { get; set; }

        internal DbSet<CashOperationsFeeHistoryData> CashOperationsFeeHistories { get; set; }

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

            modelBuilder.Entity<CashOperationsFeeData>()
                .HasIndex(x => new { x.BrokerId, x.Asset }).IsUnique();

            modelBuilder.Entity<CashOperationsFeeData>()
                .HasIndex(x => x.Asset);

            modelBuilder.Entity<TradingFeeData>()
                .HasIndex(x => new { x.BrokerId, x.AssetPair }).IsUnique();

            modelBuilder.Entity<TradingFeeData>()
                .HasIndex(x => x.BrokerId);

            modelBuilder.Entity<TradingFeeLevelData>()
                .HasIndex(x => x.Volume);

            modelBuilder.Entity<TradingFeeLevelData>()
                .HasOne<TradingFeeData>()
                .WithMany(x => x.Levels)
                .HasForeignKey(x => x.TradingFeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(x => x.CashOperationsFeeId);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(x => x.BrokerId);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(x => x.UserId);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(x => x.Asset);
        }
    }
}
