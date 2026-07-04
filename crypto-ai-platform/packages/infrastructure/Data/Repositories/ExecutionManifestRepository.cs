using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class ExecutionManifestRepository : BaseRepository<ExecutionManifest, Guid>, IExecutionManifestRepository
{
    public ExecutionManifestRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<ExecutionManifest>> GetByPackageIdAsync(Guid packageId, TenantId tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ExecutionManifests
            .Where(e => e.ReproducibilityPackageId == packageId && e.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}
