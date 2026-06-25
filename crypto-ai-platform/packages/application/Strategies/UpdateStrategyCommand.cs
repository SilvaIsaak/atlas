using MediatR;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Application.Strategies;

public record UpdateStrategyCommand : IRequest<UpdateStrategyResponse>
{
    public Guid StrategyId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
}

public record UpdateStrategyResponse
{
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public StrategyStatus Status { get; init; }
}
