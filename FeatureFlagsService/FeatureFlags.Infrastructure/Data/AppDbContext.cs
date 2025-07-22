using FeatureFlags.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlags.Infrastructure.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<FeatureFlag> FeatureFlags => Set<FeatureFlag>();
        public DbSet<EnvironmentState> EnvironmentStates => Set<EnvironmentState>();
        public DbSet<FeatureFlagAudit> AuditLogs => Set<FeatureFlagAudit>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeatureFlag>()
                .HasIndex(f => f.Name)
                .IsUnique();

            modelBuilder.Entity<EnvironmentState>()
                .HasOne(e => e.FeatureFlag)
                .WithMany(f => f.Environments)
                .HasForeignKey(e => e.FeatureFlagId);

            modelBuilder.Entity<FeatureFlagAudit>()
                .HasOne(a => a.FeatureFlag)
                .WithMany()
                .HasForeignKey(a => a.FeatureFlagId);
        }
    }

}
