using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class MarketDataSourceRepository : BaseRepository<MarketDataSource, Guid>, IMarketDataSourceRepository
{
    public MarketDataSourceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<MarketDataSource>> GetActiveAsync(TenantId tenantId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().Where(ds => ds.IsActive).ToListAsync(cancellationToken);
    }
}
