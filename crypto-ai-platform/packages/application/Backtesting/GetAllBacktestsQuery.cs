using MediatR;
using CryptoAIPlatform.Domain.Backtesting;

namespace CryptoAIPlatform.Application.Backtesting;

public record GetAllBacktestsQuery : IRequest<List<GetBacktestResponse>>
{
    public Guid UserId { get; init; }
}