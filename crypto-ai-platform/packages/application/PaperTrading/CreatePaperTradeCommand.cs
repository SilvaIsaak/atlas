using MediatR;
using CryptoAIPlatform.Domain.PaperTrading;

namespace CryptoAIPlatform.Application.PaperTrading;

public record CreatePaperTradeCommand : IRequest<CreatePaperTradeResponse>
{
    public Guid UserId { get; init; }
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public decimal InitialCapital { get; init; }
}

public record CreatePaperTradeResponse
{
    public Guid PaperTradeId { get; init; }
    public string Name { get; init; } = string.Empty;
    public PaperTradeStatus Status { get; init; }
}