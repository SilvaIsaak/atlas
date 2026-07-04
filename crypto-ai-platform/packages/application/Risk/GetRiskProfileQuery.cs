using MediatR;

namespace CryptoAIPlatform.Application.Risk;

public record GetRiskProfileQuery(Guid PortfolioId) : IRequest<IEnumerable<RiskLimitDto>>;
public record GetPortfolioRiskQuery(Guid PortfolioId) : IRequest<PortfolioRiskSnapshotDto?>;
public record GetExposureQuery(Guid PortfolioId) : IRequest<ExposureProfileDto?>;
public record GetRiskViolationsQuery(Guid PortfolioId) : IRequest<IEnumerable<RiskAssessmentDto>>;
