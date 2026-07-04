using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Risk.Repositories;

namespace CryptoAIPlatform.Application.Risk;

public class GetRiskProfileQueryHandler : IRequestHandler<GetRiskProfileQuery, IEnumerable<RiskLimitDto>>
{
    private readonly IRiskLimitRepository _riskLimitRepository;
    private readonly ILogger<GetRiskProfileQueryHandler> _logger;

    public GetRiskProfileQueryHandler(
        IRiskLimitRepository riskLimitRepository,
        ILogger<GetRiskProfileQueryHandler> logger)
    {
        _riskLimitRepository = riskLimitRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RiskLimitDto>> Handle(GetRiskProfileQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting risk profile for portfolio {PortfolioId}", request.PortfolioId);
        var limits = await _riskLimitRepository.GetByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return limits.Select(x => new RiskLimitDto(
            x.Id,
            x.PortfolioId,
            x.Name,
            x.Type.ToString(),
            x.Threshold,
            x.Severity.ToString(),
            x.Action.ToString(),
            x.IsActive)).ToList();
    }
}

public class GetPortfolioRiskQueryHandler : IRequestHandler<GetPortfolioRiskQuery, PortfolioRiskSnapshotDto?>
{
    private readonly IPortfolioRiskRepository _portfolioRiskRepository;
    private readonly ILogger<GetPortfolioRiskQueryHandler> _logger;

    public GetPortfolioRiskQueryHandler(
        IPortfolioRiskRepository portfolioRiskRepository,
        ILogger<GetPortfolioRiskQueryHandler> logger)
    {
        _portfolioRiskRepository = portfolioRiskRepository;
        _logger = logger;
    }

    public async Task<PortfolioRiskSnapshotDto?> Handle(GetPortfolioRiskQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting portfolio risk for {PortfolioId}", request.PortfolioId);
        var snapshot = await _portfolioRiskRepository.GetLatestByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return snapshot is null ? null : new PortfolioRiskSnapshotDto(
            snapshot.Id,
            snapshot.PortfolioId,
            snapshot.Timestamp,
            snapshot.Status.ToString(),
            snapshot.MarginUsage.UsedPercentage,
            snapshot.Leverage.Value);
    }
}

public class GetExposureQueryHandler : IRequestHandler<GetExposureQuery, ExposureProfileDto?>
{
    private readonly IExposureRepository _exposureRepository;
    private readonly ILogger<GetExposureQueryHandler> _logger;

    public GetExposureQueryHandler(
        IExposureRepository exposureRepository,
        ILogger<GetExposureQueryHandler> logger)
    {
        _exposureRepository = exposureRepository;
        _logger = logger;
    }

    public async Task<ExposureProfileDto?> Handle(GetExposureQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting exposure for {PortfolioId}", request.PortfolioId);
        var profile = await _exposureRepository.GetLatestByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return profile is null ? null : new ExposureProfileDto(
            profile.Id,
            profile.PortfolioId,
            profile.GeneratedAt,
            profile.TotalExposure.Value);
    }
}

public class GetRiskViolationsQueryHandler : IRequestHandler<GetRiskViolationsQuery, IEnumerable<RiskAssessmentDto>>
{
    private readonly IRiskAssessmentRepository _assessmentRepository;
    private readonly ILogger<GetRiskViolationsQueryHandler> _logger;

    public GetRiskViolationsQueryHandler(
        IRiskAssessmentRepository assessmentRepository,
        ILogger<GetRiskViolationsQueryHandler> logger)
    {
        _assessmentRepository = assessmentRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RiskAssessmentDto>> Handle(GetRiskViolationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting risk violations for portfolio {PortfolioId}", request.PortfolioId);
        var assessments = await _assessmentRepository.GetByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return assessments.Select(x => new RiskAssessmentDto(
            x.Id,
            x.PortfolioId,
            x.OrderId,
            x.Status.ToString(),
            x.Score.Score.ToString(),
            x.AssessedAt)).ToList();
    }
}
