using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Domain.Wallets;
using CryptoAIPlatform.Domain.Strategies;
using CryptoAIPlatform.Domain.Backtesting;
using CryptoAIPlatform.Domain.WalkForward;
using CryptoAIPlatform.Domain.PaperTrading;
using CryptoAIPlatform.Domain.RiskManagement;
using CryptoAIPlatform.Domain.AIDecision;
using CryptoAIPlatform.Domain.Execution;
using CryptoAIPlatform.Domain.LiveTrading;
using CryptoAIPlatform.Domain.Notifications;
using CryptoAIPlatform.Domain.Monitoring;
using CryptoAIPlatform.Domain.Learning;
using CryptoAIPlatform.Domain.Deployment;
using CryptoAIPlatform.Domain.Mobile;
using CryptoAIPlatform.Domain.Reports;
using CryptoAIPlatform.Domain.Admin;
using CryptoAIPlatform.Domain.MultiTenant;

namespace CryptoAIPlatform.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<ExchangeIntegration> ExchangeIntegrations { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletBalance> WalletBalances { get; set; }
    public DbSet<Strategy> Strategies { get; set; }
    public DbSet<Backtest> Backtests { get; set; }
    public DbSet<WalkForward> WalkForwards { get; set; }
    public DbSet<PaperTrade> PaperTrades { get; set; }
    public DbSet<RiskProfile> RiskProfiles { get; set; }
    public DbSet<AIDecision> AIDecisions { get; set; }
    public DbSet<ExecutionEngine> ExecutionEngines { get; set; }
    public DbSet<LiveTrade> LiveTrades { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<SystemMetric> SystemMetrics { get; set; }
    public DbSet<SystemAlert> SystemAlerts { get; set; }
    public DbSet<LearningContent> LearningContents { get; set; }
    public DbSet<UserLearningProgress> UserLearningProgresses { get; set; }
    public DbSet<Deployment> Deployments { get; set; }
    public DbSet<MobileDevice> MobileDevices { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<AdminLog> AdminLogs { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RoleId, rp.Permission });
            entity.HasOne<Role>()
                  .WithMany(r => r.RolePermissions)
                  .HasForeignKey(rp => rp.RoleId);
        });
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity<Guid>);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((BaseEntity<Guid>)entry.Entity).CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                ((BaseEntity<Guid>)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
