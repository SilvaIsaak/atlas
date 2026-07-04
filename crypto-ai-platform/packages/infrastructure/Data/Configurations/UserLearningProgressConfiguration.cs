using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Learning;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class UserLearningProgressConfiguration : IEntityTypeConfiguration<UserLearningProgress>
{
    public void Configure(EntityTypeBuilder<UserLearningProgress> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.LearningContent)
            .WithMany()
            .HasForeignKey(x => x.LearningContentId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.TenantId);
    }
}
