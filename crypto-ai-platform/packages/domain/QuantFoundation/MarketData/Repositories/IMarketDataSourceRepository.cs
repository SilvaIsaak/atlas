using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.Repositories;

public interface IMarketDataSourceRepository
{
    Task<MarketDataSource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MarketDataSource>> GetAllAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MarketDataSource>> GetActiveAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(MarketDataSource dataSource, CancellationToken cancellationToken = default);
    Task UpdateAsync(MarketDataSource dataSource, CancellationToken cancellationToken = default);
}