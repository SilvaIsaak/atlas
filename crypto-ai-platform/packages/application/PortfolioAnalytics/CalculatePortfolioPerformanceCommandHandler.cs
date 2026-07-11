using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.PortfolioAnalytics;
using CryptoAIPlatform.Domain.PortfolioAnalytics.Repositories;
using CryptoAIPlatform.Domain.PortfolioAnalytics.Services;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.PortfolioAnalytics.ValueObjects;

namespace CryptoAIPlatform.Application.PortfolioAnalytics;

public class CalculatePortfolioPerformanceCommandHandler : IRequestHandler<CalculatePortfolioPerformanceCommand, PortfolioAnalyticsDto>
{
    private readonly IPortfolioAnalyticsRepository _repository;
    private readonly IPortfolioAnalyticsService _service;
    private readonly ILogger<CalculatePortfolioPerformanceCommandHandler> _logger;

    public CalculatePortfolioPerformanceCommandHandler(
        IPortfolioAnalyticsRepository repository,
        IPortfolioAnalyticsService service,
        ILogger<CalculatePortfolioPerformanceCommandHandler> logger)
    {
        _repository = repository;
        _service = service;
        _logger = logger;
    }

    public async Task<PortfolioAnalyticsDto> Handle(CalculatePortfolioPerformanceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Calculating performance for portfolio {PortfolioId}", request.PortfolioId);
        await _service.CalculatePortfolioPerformanceAsync(TenantId.Default, request.PortfolioId, cancellationToken);

        var analytics = await _repository.GetByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return new PortfolioAnalyticsDto(
            analytics!.Id,
            analytics.PortfolioId,
            analytics.CalculatedAt,
            analytics.SharpeRatio?.Value,
            analytics.SortinoRatio?.Value,
            analytics.CalmarRatio?.Value,
            analytics.ProfitFactor?.Value,
            analytics.WinRate?.Percentage,
            analytics.Volatility?.Annualized);
    }
}

public class RecalculatePortfolioMetricsCommandHandler : IRequestHandler<RecalculatePortfolioMetricsCommand, PortfolioAnalyticsDto>
{
    private readonly IPortfolioAnalyticsRepository _repository;
    private readonly IPortfolioAnalyticsService _service;
    private readonly ILogger<RecalculatePortfolioMetricsCommandHandler> _logger;

    public RecalculatePortfolioMetricsCommandHandler(
        IPortfolioAnalyticsRepository repository,
        IPortfolioAnalyticsService service,
        ILogger<RecalculatePortfolioMetricsCommandHandler> logger)
    {
        _repository = repository;
        _service = service;
        _logger = logger;
    }

    public async Task<PortfolioAnalyticsDto> Handle(RecalculatePortfolioMetricsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recalculating metrics for portfolio {PortfolioId}", request.PortfolioId);
        await _service.RecalculatePortfolioMetricsAsync(TenantId.Default, request.PortfolioId, cancellationToken);

        var analytics = await _repository.GetByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return new PortfolioAnalyticsDto(
            analytics!.Id,
            analytics.PortfolioId,
            analytics.CalculatedAt,
            analytics.SharpeRatio?.Value,
            analytics.SortinoRatio?.Value,
            analytics.CalmarRatio?.Value,
            analytics.ProfitFactor?.Value,
            analytics.WinRate?.Percentage,
            analytics.Volatility?.Annualized);
    }
}

public class GetPortfolioPerformanceQueryHandler : IRequestHandler<GetPortfolioPerformanceQuery, PortfolioAnalyticsDto?>
{
    private readonly IPortfolioAnalyticsRepository _repository;
    private readonly ILogger<GetPortfolioPerformanceQueryHandler> _logger;

    public GetPortfolioPerformanceQueryHandler(
        IPortfolioAnalyticsRepository repository,
        ILogger<GetPortfolioPerformanceQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PortfolioAnalyticsDto?> Handle(GetPortfolioPerformanceQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting performance for portfolio {PortfolioId}", request.PortfolioId);
        var analytics = await _repository.GetByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        if (analytics == null) return null;

        return new PortfolioAnalyticsDto(
            analytics.Id,
            analytics.PortfolioId,
            analytics.CalculatedAt,
            analytics.SharpeRatio?.Value,
            analytics.SortinoRatio?.Value,
            analytics.CalmarRatio?.Value,
            analytics.ProfitFactor?.Value,
            analytics.WinRate?.Percentage,
            analytics.Volatility?.Annualized);
    }
}

public class GetEquityCurveQueryHandler : IRequestHandler<GetEquityCurveQuery, IEnumerable<EquityCurvePointDto>>
{
    private readonly IPortfolioAnalyticsRepository _repository;
    private readonly ILogger<GetEquityCurveQueryHandler> _logger;

    public GetEquityCurveQueryHandler(
        IPortfolioAnalyticsRepository repository,
        ILogger<GetEquityCurveQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<EquityCurvePointDto>> Handle(GetEquityCurveQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting equity curve for portfolio {PortfolioId}", request.PortfolioId);
        var snapshot = await _repository.GetLatestSnapshotByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return snapshot?.EquityCurve.Select(x => new EquityCurvePointDto(x.Timestamp, x.Equity)).ToList() ?? new List<EquityCurvePointDto>();
    }
}

public class GetPerformanceHistoryQueryHandler : IRequestHandler<GetPerformanceHistoryQuery, IEnumerable<PerformanceSnapshotDto>>
{
    private readonly IPortfolioAnalyticsRepository _repository;
    private readonly ILogger<GetPerformanceHistoryQueryHandler> _logger;

    public GetPerformanceHistoryQueryHandler(
        IPortfolioAnalyticsRepository repository,
        ILogger<GetPerformanceHistoryQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<PerformanceSnapshotDto>> Handle(GetPerformanceHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting performance history for portfolio {PortfolioId}", request.PortfolioId);
        var analytics = await _repository.GetByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return analytics?.Snapshots.Select(x => new PerformanceSnapshotDto(
            x.Id,
            x.PortfolioId,
            x.Timestamp,
            x.TotalEquity,
            x.TotalReturn,
            x.DailyReturn,
            x.MonthlyReturn,
            x.AnnualReturn)).ToList() ?? new List<PerformanceSnapshotDto>();
    }
}
