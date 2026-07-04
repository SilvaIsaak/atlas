using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class MarketDataIngestionJobRepository : BaseRepository<MarketDataIngestionJob, Guid>, IMarketDataIngestionJobRepository
{
    public MarketDataIngestionJobRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<MarketDataIngestionJob>> GetPendingAsync(TenantId tenantId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Where(j => j.Status == Domain.QuantFoundation.MarketData.MarketDataIngestionStatus.Pending)
            .ToListAsync(cancellationToken);
    }
}
