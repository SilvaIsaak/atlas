using MediatR;

namespace CryptoAIPlatform.Application.RiskManagement;

public record GetAllRiskProfilesQuery : IRequest<List<GetRiskProfileResponse>>
{
    public Guid UserId { get; init; }
}