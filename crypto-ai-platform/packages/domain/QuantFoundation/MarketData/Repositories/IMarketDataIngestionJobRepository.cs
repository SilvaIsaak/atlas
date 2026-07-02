namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.Repositories;

public interface IMarketDataIngestionJobRepository
{
    Task<MarketDataIngestionJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MarketDataIngestionJob>> GetByDataSourceIdAsync(Guid dataSourceId, CancellationToken cancellationToken = default);
    Task AddAsync(MarketDataIngestionJob job, CancellationToken cancellationToken = default);
    Task UpdateAsync(MarketDataIngestionJob job, CancellationToken cancellationToken = default);
}