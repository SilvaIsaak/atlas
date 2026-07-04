using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class FeatureRepository : BaseRepository<Feature, Guid>, IFeatureRepository
{
    public FeatureRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Feature?> GetByNameAsync(TenantId tenantId, string name, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(f => f.Name == name, cancellationToken);
    }

    public async Task<IReadOnlyList<Feature>> GetApprovedAsync(TenantId tenantId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Where(f => f.Maturity == Domain.QuantFoundation.FeatureStore.FeatureMaturity.Approved)
            .ToListAsync(cancellationToken);
    }
}
