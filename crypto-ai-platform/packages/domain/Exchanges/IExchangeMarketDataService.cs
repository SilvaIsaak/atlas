namespace CryptoAIPlatform.Domain.Exchanges;

public interface IExchangeMarketDataService
{
    Task<ExchangeTicker> GetTickerAsync(string symbol, CancellationToken cancellationToken = default);
    Task<ExchangeOrderBook> GetOrderBookAsync(string symbol, int limit = 20, CancellationToken cancellationToken = default);
    Task<List<ExchangeKline>> GetKlinesAsync(string symbol, string interval, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default);
    Task<ExchangeInfo> GetExchangeInfoAsync(CancellationToken cancellationToken = default);
    Task<DateTime> GetServerTimeAsync(CancellationToken cancellationToken = default);
    Task<bool> PingAsync(CancellationToken cancellationToken = default);
    Task<List<ExchangeSymbol>> GetSymbolsAsync(CancellationToken cancellationToken = default);
    Task<List<ExchangeTicker>> GetTickersAsync(CancellationToken cancellationToken = default);
    Task<List<ExchangeTrade>> GetRecentTradesAsync(string symbol, int limit = 500, CancellationToken cancellationToken = default);
    Task<List<ExchangeTrade>> GetHistoricalTradesAsync(string symbol, int limit = 500, CancellationToken cancellationToken = default);
    Task<Exchange24hStatistics> Get24hStatisticsAsync(string symbol, CancellationToken cancellationToken = default);
    Task<FundingRateData> GetFundingRateAsync(string symbol, CancellationToken cancellationToken = default);
    Task<OpenInterestData> GetOpenInterestAsync(string symbol, CancellationToken cancellationToken = default);
    Task<MarkPriceData> GetMarkPriceAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IndexPriceData> GetIndexPriceAsync(string symbol, CancellationToken cancellationToken = default);
}
