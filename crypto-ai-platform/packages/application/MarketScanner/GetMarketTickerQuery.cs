using MediatR;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Exchanges;

namespace CryptoAIPlatform.Application.MarketScanner;

public record GetMarketTickerQuery(string ExchangeCode, string Symbol) : IRequest<ExchangeTicker>;

public class GetMarketTickerQueryHandler : IRequestHandler<GetMarketTickerQuery, ExchangeTicker>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public GetMarketTickerQueryHandler(IExchangeClientFactory exchangeClientFactory)
    {
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<ExchangeTicker> Handle(GetMarketTickerQuery request, CancellationToken cancellationToken)
    {
        var client = _exchangeClientFactory.CreateClient(request.ExchangeCode, "", ""); // Public endpoint doesn't need API keys
        return await client.MarketDataService.GetTickerAsync(request.Symbol, cancellationToken);
    }
}
