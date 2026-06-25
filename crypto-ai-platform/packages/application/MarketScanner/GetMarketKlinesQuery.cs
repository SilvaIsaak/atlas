using MediatR;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Exchanges;

namespace CryptoAIPlatform.Application.MarketScanner;

public record GetMarketKlinesQuery(string ExchangeCode, string Symbol, string Interval, DateTime? StartTime = null, DateTime? EndTime = null) : IRequest<List<ExchangeKline>>;

public class GetMarketKlinesQueryHandler : IRequestHandler<GetMarketKlinesQuery, List<ExchangeKline>>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public GetMarketKlinesQueryHandler(IExchangeClientFactory exchangeClientFactory)
    {
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<List<ExchangeKline>> Handle(GetMarketKlinesQuery request, CancellationToken cancellationToken)
    {
        var client = _exchangeClientFactory.CreateClient(request.ExchangeCode, "", ""); // Public endpoint doesn't need API keys
        var startTime = request.StartTime ?? DateTime.UtcNow.AddDays(-7);
        var endTime = request.EndTime ?? DateTime.UtcNow;
        return await client.GetKlinesAsync(request.Symbol, request.Interval, startTime, endTime, cancellationToken);
    }
}
