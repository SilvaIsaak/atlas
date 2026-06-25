using MediatR;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Application.Strategies;

public record GetAllStrategiesQuery : IRequest<List<GetStrategyResponse>>
{
    public Guid UserId { get; init; }
}
