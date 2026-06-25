using MediatR;
using CryptoAIPlatform.Domain.PaperTrading;

namespace CryptoAIPlatform.Application.PaperTrading;

public record GetPaperTradeQuery : IRequest<GetPaperTradeResponse>
{
    public Guid PaperTradeId { get; init; }
    public Guid UserId { get; init; }
}

public record GetPaperTradeResponse(
    Guid PaperTradeId,
    Guid UserId,
    Guid StrategyId,
    string Name,
    string Description,
    string AssetSymbol,
    decimal InitialCapital,
    decimal CurrentCapital,
    PaperTradeStatus Status,
    List<PaperTradeOrder>? Orders,
    decimal? TotalReturn,
    DateTime? StartedAt,
    DateTime? StoppedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);