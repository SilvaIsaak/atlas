using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.PortfolioAnalytics.Events;

public class PortfolioPerformanceUpdatedV1 : DomainEvent
{
    public Guid PortfolioId { get; init; }
    public DateTime CalculatedAt { get; init; }

    public PortfolioPerformanceUpdatedV1(TenantId tenantId, Guid portfolioId, DateTime calculatedAt)
    {
        TenantId = tenantId;
        PortfolioId = portfolioId;
        CalculatedAt = calculatedAt;
    }
}
