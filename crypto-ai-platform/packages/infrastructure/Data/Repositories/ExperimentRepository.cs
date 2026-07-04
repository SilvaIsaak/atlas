using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class ExperimentRepository : BaseRepository<Experiment, Guid>, IExperimentRepository
{
    public ExperimentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Experiment>> GetByUserIdAsync(TenantId tenantId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().Where(e => e.CreatedBy == userId).ToListAsync(cancellationToken);
    }
}
