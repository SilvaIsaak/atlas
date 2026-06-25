using MediatR;

namespace CryptoAIPlatform.Application.PaperTrading;

public record GetAllPaperTradesQuery : IRequest<List<GetPaperTradeResponse>>
{
    public Guid UserId { get; init; }
}