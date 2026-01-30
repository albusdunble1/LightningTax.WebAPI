using LightningTax.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace LightningTax.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // =======================
        // DbSets
        // =======================

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<CompanyYear> CompanyYears => Set<CompanyYear>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<CapitalAllowance> CapitalAllowances => Set<CapitalAllowance>();
        public DbSet<CapitalAllowancePool> CapitalAllowancePools => Set<CapitalAllowancePool>();
        public DbSet<Schedule3Output> Schedule3Outputs => Set<Schedule3Output>();
        public DbSet<CaAuditTrail> CaAuditTrails => Set<CaAuditTrail>();

        // =======================
        // Model Configuration
        // =======================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------
            // Companies
            // -----------------------
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CompanyNumber)
                      .IsUnique();

                entity.Property(e => e.PaidUpCapital)
                      .HasPrecision(15, 2);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("now()");

                entity.Property(e => e.ResidentStatus)
                      .HasConversion<string>();
            });

            // -----------------------
            // CompanyYears
            // -----------------------
            modelBuilder.Entity<CompanyYear>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.CompanyId, e.YearOfAssessment })
                      .IsUnique();

                entity.HasOne(e => e.Company)
                      .WithMany()
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Status)
                      .HasDefaultValue("draft");

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("now()");
            });

            // -----------------------
            // Assets
            // -----------------------
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CompanyId);

                entity.HasOne(e => e.Company)
                      .WithMany()
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.OriginalCost)
                      .HasPrecision(15, 2);

                entity.Property(e => e.QualifyingExpenditure)
                      .HasPrecision(15, 2);

                entity.Property(e => e.BusinessUsePct)
                      .HasPrecision(5, 2);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("now()");
            });

            // -----------------------
            // CapitalAllowances
            // -----------------------
            modelBuilder.Entity<CapitalAllowance>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CompanyYearId);
                entity.HasIndex(e => e.AssetId);

                entity.HasIndex(e => new { e.CompanyYearId, e.AssetId })
                      .IsUnique();

                entity.Property(e => e.OpeningTwdv).HasPrecision(15, 2);
                entity.Property(e => e.UnabsorbedCaBf).HasPrecision(15, 2);
                entity.Property(e => e.QeCurrentYear).HasPrecision(15, 2);
                entity.Property(e => e.IaAmount).HasPrecision(15, 2);
                entity.Property(e => e.AaAmount).HasPrecision(15, 2);
                entity.Property(e => e.RestrictedCa).HasPrecision(15, 2);
                entity.Property(e => e.ClosingTwdv).HasPrecision(15, 2);
                entity.Property(e => e.UnabsorbedCaCf).HasPrecision(15, 2);

                entity.Property(e => e.ComputedAt)
                      .HasDefaultValueSql("now()");
            });

            // -----------------------
            // CapitalAllowancePools
            // -----------------------
            modelBuilder.Entity<CapitalAllowancePool>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.OpeningBalance).HasPrecision(15, 2);
                entity.Property(e => e.Additions).HasPrecision(15, 2);
                entity.Property(e => e.Disposals).HasPrecision(15, 2);
                entity.Property(e => e.AaRate).HasPrecision(5, 2);
                entity.Property(e => e.AaAmount).HasPrecision(15, 2);
                entity.Property(e => e.ClosingBalance).HasPrecision(15, 2);
            });

            // -----------------------
            // Schedule3Outputs
            // -----------------------
            modelBuilder.Entity<Schedule3Output>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CompanyYearId)
                      .IsUnique();

                entity.Property(e => e.GeneratedAt)
                      .HasDefaultValueSql("now()");
            });

            // -----------------------
            // CaAuditTrails
            // -----------------------
            modelBuilder.Entity<CaAuditTrail>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("now()");
            });
        }
    }
}
