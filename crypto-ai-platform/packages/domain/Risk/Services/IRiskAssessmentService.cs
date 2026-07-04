using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Risk;

namespace CryptoAIPlatform.Domain.Risk.Services;

public interface IRiskAssessmentService
{
    Task<RiskAssessment> AssessOrderRiskAsync(TenantId tenantId, Guid portfolioId, Guid orderId, CancellationToken cancellationToken = default);
    Task<IEnumerable<RiskAssessment>> GetAssessmentsByPortfolioAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default);
}

public interface IRiskValidationService
{
    Task<bool> ValidateOrderRiskAsync(TenantId tenantId, Guid portfolioId, Guid orderId, CancellationToken cancellationToken = default);
    Task<bool> IsOrderAllowedAsync(TenantId tenantId, Guid portfolioId, Guid orderId, CancellationToken cancellationToken = default);
}

public interface IExposureService
{
    Task<ExposureProfile> CalculateExposureAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default);
    Task<ExposureProfile?> GetLatestExposureAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default);
}

public interface IMarginService
{
    Task<MarginRequirement> CalculateMarginRequirementAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default);
    Task<MarginRequirement?> GetLatestMarginRequirementAsync(TenantId tenantId, Guid portfolioId, CancellationToken cancellationToken = default);
}

public interface ILiquidationService
{
    Task<LiquidationLevel> CalculateLiquidationLevelAsync(TenantId tenantId, Guid positionId, CancellationToken cancellationToken = default);
    Task<LiquidationLevel?> GetLatestLiquidationLevelAsync(TenantId tenantId, Guid positionId, CancellationToken cancellationToken = default);
}
