namespace CryptoAIPlatform.Domain.PortfolioAnalytics.Repositories;

public interface IPortfolioAnalyticsRepository
{
    Task<PortfolioPerformanceReport?> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task<PerformanceSnapshot?> GetLatestSnapshotByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(PortfolioPerformanceReport analytics, CancellationToken cancellationToken = default);
    Task AddAsync(PerformanceSnapshot snapshot, CancellationToken cancellationToken = default);
    Task UpdateAsync(PortfolioPerformanceReport analytics, CancellationToken cancellationToken = default);
}
