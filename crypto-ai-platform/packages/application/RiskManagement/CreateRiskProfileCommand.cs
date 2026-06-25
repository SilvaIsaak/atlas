using MediatR;
using CryptoAIPlatform.Domain.RiskManagement;

namespace CryptoAIPlatform.Application.RiskManagement;

public record CreateRiskProfileCommand : IRequest<CreateRiskProfileResponse>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public List<RiskRule>? Rules { get; init; }
}

public record CreateRiskProfileResponse
{
    public Guid RiskProfileId { get; init; }
    public string Name { get; init; } = string.Empty;
}