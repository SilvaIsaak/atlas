using MediatR;

namespace CryptoAIPlatform.Application.LiveTrading;

public record GetAllLiveTradesQuery : IRequest<List<GetLiveTradeResponse>>
{
    public Guid UserId { get; init; }
}