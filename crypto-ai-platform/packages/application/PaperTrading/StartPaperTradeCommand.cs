using MediatR;

namespace CryptoAIPlatform.Application.PaperTrading;

public record StartPaperTradeCommand : IRequest<StartPaperTradeResponse>
{
    public Guid PaperTradeId { get; init; }
    public Guid UserId { get; init; }
}

public record StartPaperTradeResponse
{
    public Guid PaperTradeId { get; init; }
    public string Status { get; init; } = string.Empty;
}