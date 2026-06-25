using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Domain.Wallets;

namespace CryptoAIPlatform.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<ExchangeIntegration> ExchangeIntegrations { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletBalance> WalletBalances { get; set; }
    
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
