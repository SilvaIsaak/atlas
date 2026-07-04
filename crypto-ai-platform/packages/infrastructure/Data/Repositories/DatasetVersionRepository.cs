using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class DatasetVersionRepository : BaseRepository<DatasetVersion, Guid>, IDatasetVersionRepository
{
    public DatasetVersionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<DatasetVersion>> GetByDatasetIdAsync(Guid datasetId, TenantId tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DatasetVersions
            .Where(v => v.DatasetId == datasetId && v.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}
