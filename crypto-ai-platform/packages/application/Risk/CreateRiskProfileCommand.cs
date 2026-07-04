using MediatR;

namespace CryptoAIPlatform.Application.Risk;

public record CreateRiskProfileCommand(Guid PortfolioId) : IRequest<PortfolioRiskSnapshotDto>;
public record ValidateOrderRiskCommand(Guid OrderId, Guid PortfolioId) : IRequest<RiskAssessmentDto>;
public record UpdateExposureCommand(Guid PortfolioId) : IRequest<ExposureProfileDto>;
public record TriggerMarginCallCommand(Guid PortfolioId) : IRequest;
public record UpdatePortfolioRiskCommand(Guid PortfolioId) : IRequest<PortfolioRiskSnapshotDto>;
