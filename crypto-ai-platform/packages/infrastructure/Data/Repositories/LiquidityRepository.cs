using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class LiquidityRepository : BaseRepository<LiquiditySnapshot, Guid>, ILiquidityRepository
{
    public LiquidityRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<LiquiditySnapshot>> GetByModelIdAsync(Guid modelId, TenantId tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LiquiditySnapshots
            .Where(x => x.ModelId == modelId && x.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}
