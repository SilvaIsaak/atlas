using MediatR;
using CryptoAIPlatform.Domain.RiskManagement;

namespace CryptoAIPlatform.Application.RiskManagement;

public record GetRiskProfileQuery : IRequest<GetRiskProfileResponse>
{
    public Guid RiskProfileId { get; init; }
    public Guid UserId { get; init; }
}

public record GetRiskProfileResponse(
    Guid RiskProfileId,
    Guid UserId,
    string Name,
    List<RiskRule>? Rules,
    List<RiskAlert>? Alerts,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);