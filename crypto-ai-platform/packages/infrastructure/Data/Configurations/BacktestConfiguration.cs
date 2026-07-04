using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Backtesting;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class BacktestConfiguration : IEntityTypeConfiguration<Backtest>
{
    public void Configure(EntityTypeBuilder<Backtest> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.HasIndex(x => x.TenantId);
    }
}
