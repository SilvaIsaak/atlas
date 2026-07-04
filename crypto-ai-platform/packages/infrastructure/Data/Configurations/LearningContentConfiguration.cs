using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Learning;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class LearningContentConfiguration : IEntityTypeConfiguration<LearningContent>
{
    public void Configure(EntityTypeBuilder<LearningContent> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);
            
        builder.HasIndex(x => x.TenantId);
    }
}
