using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.PortfolioAnalytics;
using CryptoAIPlatform.Domain.PortfolioAnalytics.Repositories;
using CryptoAIPlatform.Domain.PortfolioAnalytics.Services;
using CryptoAIPlatform.Domain.PortfolioAnalytics.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.PortfolioAnalytics;

public class PortfolioAnalyticsService : IPortfolioAnalyticsService
{
    private readonly IPortfolioAnalyticsRepository _repository;
    private readonly ILogger<PortfolioAnalyticsService> _logger;

    public PortfolioAnalyticsService(
        IPortfolioAnalyticsRepository repository,
        ILogger<PortfolioAnalyticsService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CalculatePortfolioPerformanceAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Calculating portfolio performance for {PortfolioId}", portfolioId);

        var analytics = PortfolioPerformanceReport.Create(Guid.NewGuid(), tenantId, portfolioId, DateTime.UtcNow);
        await _repository.AddAsync(analytics, cancellationToken);
    }

    public async Task RecalculatePortfolioMetricsAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Recalculating portfolio metrics for {PortfolioId}", portfolioId);

        var analytics = await _repository.GetByPortfolioIdAsync(portfolioId, cancellationToken);
        if (analytics != null)
        {
            analytics.UpdateMetrics(
                new SharpeRatio(0, 0, 0),
                new SortinoRatio(0, 0, 0),
                new CalmarRatio(0, 0),
                new ProfitFactor(0),
                new WinRate(0),
                new Volatility(0),
                new Expectancy(0, 0, 0));

            await _repository.UpdateAsync(analytics, cancellationToken);
        }
    }
}
