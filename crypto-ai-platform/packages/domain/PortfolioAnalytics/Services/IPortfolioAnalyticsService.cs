using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.PortfolioAnalytics.Services;

public interface IPortfolioAnalyticsService
{
    Task CalculatePortfolioPerformanceAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default);
    Task RecalculatePortfolioMetricsAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default);
}
