using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Execution;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExecutionEngineConfiguration : IEntityTypeConfiguration<ExecutionEngine>
{
    public void Configure(EntityTypeBuilder<ExecutionEngine> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.TenantId);
    }
}
