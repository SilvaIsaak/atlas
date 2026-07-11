using MediatR;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Exchanges;

namespace CryptoAIPlatform.Application.MarketScanner;

public record GetMarketOrderBookQuery(string ExchangeCode, string Symbol, int Limit = 20) : IRequest<ExchangeOrderBook>;

public class GetMarketOrderBookQueryHandler : IRequestHandler<GetMarketOrderBookQuery, ExchangeOrderBook>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public GetMarketOrderBookQueryHandler(IExchangeClientFactory exchangeClientFactory)
    {
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<ExchangeOrderBook> Handle(GetMarketOrderBookQuery request, CancellationToken cancellationToken)
    {
        var client = _exchangeClientFactory.CreateClient(request.ExchangeCode, "", ""); // Public endpoint doesn't need API keys
        return await client.MarketDataService.GetOrderBookAsync(request.Symbol, request.Limit, cancellationToken);
    }
}
