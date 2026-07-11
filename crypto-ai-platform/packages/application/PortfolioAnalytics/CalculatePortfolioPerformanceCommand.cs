using MediatR;

namespace CryptoAIPlatform.Application.PortfolioAnalytics;

public record CalculatePortfolioPerformanceCommand(Guid PortfolioId) : IRequest<PortfolioAnalyticsDto>;
public record RecalculatePortfolioMetricsCommand(Guid PortfolioId) : IRequest<PortfolioAnalyticsDto>;

public record GetPortfolioPerformanceQuery(Guid PortfolioId) : IRequest<PortfolioAnalyticsDto?>;
public record GetEquityCurveQuery(Guid PortfolioId) : IRequest<IEnumerable<EquityCurvePointDto>>;
public record GetPerformanceHistoryQuery(Guid PortfolioId) : IRequest<IEnumerable<PerformanceSnapshotDto>>;
