using MediatR;

namespace CryptoAIPlatform.Application.LiveTrading;

public record StartLiveTradeCommand : IRequest<StartLiveTradeResponse>
{
    public Guid LiveTradeId { get; init; }
    public Guid UserId { get; init; }
}

public record StartLiveTradeResponse
{
    public Guid LiveTradeId { get; init; }
    public string Status { get; init; } = string.Empty;
}