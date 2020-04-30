using Fees.Repositories.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fees.Repositories.Context
{
    public class DataContext : DbContext
    {
        private const string Schema = "fees";

        private string _connectionString;

        public DataContext()
        {
        }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal DbSet<CashOperationsFeeData> CashOperationsFees { get; set; }

        internal DbSet<CashOperationsFeeHistoryData> CashOperationsFeeHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString == null)
            {
                System.Console.Write("Enter connection string: ");
                _connectionString = System.Console.ReadLine();
            }

            optionsBuilder.UseNpgsql(_connectionString,
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<CashOperationsFeeData>()
                .HasIndex(p => new { p.BrokerId, p.Asset }).IsUnique();

            modelBuilder.Entity<CashOperationsFeeData>()
                .HasIndex(b => b.Asset);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(b => b.CashOperationsFeeId);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(b => b.BrokerId);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(b => b.UserId);

            modelBuilder.Entity<CashOperationsFeeHistoryData>()
                .HasIndex(b => b.Asset);
        }
    }
}
