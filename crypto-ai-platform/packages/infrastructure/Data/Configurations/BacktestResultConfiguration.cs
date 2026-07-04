using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Backtesting;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class BacktestResultConfiguration : IEntityTypeConfiguration<BacktestResult>
{
    public void Configure(EntityTypeBuilder<BacktestResult> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Backtest)
            .WithMany()
            .HasForeignKey(x => x.BacktestId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.TenantId);
    }
}
