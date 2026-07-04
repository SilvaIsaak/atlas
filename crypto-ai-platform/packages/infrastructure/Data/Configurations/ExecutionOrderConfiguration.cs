using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Execution;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExecutionOrderConfiguration : IEntityTypeConfiguration<ExecutionOrder>
{
    public void Configure(EntityTypeBuilder<ExecutionOrder> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.ExecutionEngine)
            .WithMany()
            .HasForeignKey(x => x.ExecutionEngineId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.TenantId);
    }
}
