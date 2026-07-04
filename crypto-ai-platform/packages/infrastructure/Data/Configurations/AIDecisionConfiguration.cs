using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.AIDecision;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class AIDecisionConfiguration : IEntityTypeConfiguration<AIDecision>
{
    public void Configure(EntityTypeBuilder<AIDecision> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.TenantId);
    }
}
