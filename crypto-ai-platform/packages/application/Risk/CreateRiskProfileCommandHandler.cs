using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Risk;
using CryptoAIPlatform.Domain.Risk.Repositories;
using CryptoAIPlatform.Domain.Risk.ValueObjects;
using CryptoAIPlatform.Domain.Risk.Enums;

namespace CryptoAIPlatform.Application.Risk;

public class CreateRiskProfileCommandHandler : IRequestHandler<CreateRiskProfileCommand, PortfolioRiskSnapshotDto>
{
    private readonly IPortfolioRiskRepository _portfolioRiskRepository;
    private readonly ILogger<CreateRiskProfileCommandHandler> _logger;

    public CreateRiskProfileCommandHandler(
        IPortfolioRiskRepository portfolioRiskRepository,
        ILogger<CreateRiskProfileCommandHandler> logger)
    {
        _portfolioRiskRepository = portfolioRiskRepository;
        _logger = logger;
    }

    public async Task<PortfolioRiskSnapshotDto> Handle(CreateRiskProfileCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating risk profile for portfolio {PortfolioId}", request.PortfolioId);

        var snapshot = PortfolioRiskSnapshot.Create(
            id: Guid.NewGuid(),
            tenantId: TenantId.Default,
            portfolioId: request.PortfolioId,
            timestamp: DateTime.UtcNow,
            status: RiskStatus.Green,
            marginUsage: new MarginUsage(0, 10000, 0),
            leverage: new PortfolioLeverage(1),
            var: null,
            expectedShortfall: null,
            drawdown: null);

        await _portfolioRiskRepository.AddAsync(snapshot, cancellationToken);

        return new PortfolioRiskSnapshotDto(
            snapshot.Id,
            snapshot.PortfolioId,
            snapshot.Timestamp,
            snapshot.Status.ToString(),
            snapshot.MarginUsage.UsedPercentage,
            snapshot.Leverage.Value);
    }
}

public class ValidateOrderRiskCommandHandler : IRequestHandler<ValidateOrderRiskCommand, RiskAssessmentDto>
{
    private readonly IRiskAssessmentRepository _assessmentRepository;
    private readonly ILogger<ValidateOrderRiskCommandHandler> _logger;

    public ValidateOrderRiskCommandHandler(
        IRiskAssessmentRepository assessmentRepository,
        ILogger<ValidateOrderRiskCommandHandler> logger)
    {
        _assessmentRepository = assessmentRepository;
        _logger = logger;
    }

    public async Task<RiskAssessmentDto> Handle(ValidateOrderRiskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating risk for order {OrderId}", request.OrderId);

        var assessment = RiskAssessment.Create(
            id: Guid.NewGuid(),
            tenantId: TenantId.Default,
            portfolioId: request.PortfolioId,
            orderId: request.OrderId,
            status: RiskStatus.Green,
            score: new RiskScore(0, RiskStatus.Green),
            assessedAt: DateTime.UtcNow);

        await _assessmentRepository.AddAsync(assessment, cancellationToken);

        return new RiskAssessmentDto(
            assessment.Id,
            assessment.PortfolioId,
            assessment.OrderId,
            assessment.Status.ToString(),
            assessment.Score.Score.ToString(),
            assessment.AssessedAt);
    }
}

public class UpdateExposureCommandHandler : IRequestHandler<UpdateExposureCommand, ExposureProfileDto>
{
    private readonly IExposureRepository _exposureRepository;
    private readonly ILogger<UpdateExposureCommandHandler> _logger;

    public UpdateExposureCommandHandler(
        IExposureRepository exposureRepository,
        ILogger<UpdateExposureCommandHandler> logger)
    {
        _exposureRepository = exposureRepository;
        _logger = logger;
    }

    public async Task<ExposureProfileDto> Handle(UpdateExposureCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating exposure for portfolio {PortfolioId}", request.PortfolioId);

        var profile = ExposureProfile.Create(
            id: Guid.NewGuid(),
            tenantId: TenantId.Default,
            portfolioId: request.PortfolioId,
            generatedAt: DateTime.UtcNow,
            totalExposure: new MaxExposure(0),
            highestConcentration: null);

        await _exposureRepository.AddAsync(profile, cancellationToken);

        return new ExposureProfileDto(
            profile.Id,
            profile.PortfolioId,
            profile.GeneratedAt,
            profile.TotalExposure.Value);
    }
}

public class TriggerMarginCallCommandHandler : IRequestHandler<TriggerMarginCallCommand>
{
    private readonly ILogger<TriggerMarginCallCommandHandler> _logger;

    public TriggerMarginCallCommandHandler(ILogger<TriggerMarginCallCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(TriggerMarginCallCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Triggering margin call for portfolio {PortfolioId}", request.PortfolioId);
        return Task.FromResult(Unit.Value);
    }
}

public class UpdatePortfolioRiskCommandHandler : IRequestHandler<UpdatePortfolioRiskCommand, PortfolioRiskSnapshotDto>
{
    private readonly IPortfolioRiskRepository _portfolioRiskRepository;
    private readonly ILogger<UpdatePortfolioRiskCommandHandler> _logger;

    public UpdatePortfolioRiskCommandHandler(
        IPortfolioRiskRepository portfolioRiskRepository,
        ILogger<UpdatePortfolioRiskCommandHandler> logger)
    {
        _portfolioRiskRepository = portfolioRiskRepository;
        _logger = logger;
    }

    public async Task<PortfolioRiskSnapshotDto> Handle(UpdatePortfolioRiskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating portfolio risk for portfolio {PortfolioId}", request.PortfolioId);

        var snapshot = PortfolioRiskSnapshot.Create(
            id: Guid.NewGuid(),
            tenantId: TenantId.Default,
            portfolioId: request.PortfolioId,
            timestamp: DateTime.UtcNow,
            status: RiskStatus.Green,
            marginUsage: new MarginUsage(0, 10000, 0),
            leverage: new PortfolioLeverage(1),
            var: null,
            expectedShortfall: null,
            drawdown: null);

        await _portfolioRiskRepository.AddAsync(snapshot, cancellationToken);

        return new PortfolioRiskSnapshotDto(
            snapshot.Id,
            snapshot.PortfolioId,
            snapshot.Timestamp,
            snapshot.Status.ToString(),
            snapshot.MarginUsage.UsedPercentage,
            snapshot.Leverage.Value);
    }
}
