namespace CryptoAIPlatform.Domain.Exchanges;

public interface IExchangeStreamingService
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
    event EventHandler<ExchangeTrade>? OnTradeReceived;
    event EventHandler<ExchangeOrderBook>? OnOrderBookReceived;
    event EventHandler<ExchangeTicker>? OnMiniTickerReceived;
    event EventHandler<ExchangeTicker>? OnTickerReceived;
    event EventHandler<ExchangeKline>? OnKlineReceived;
    Task SubscribeToTradesAsync(string symbol, CancellationToken cancellationToken = default);
    Task SubscribeToOrderBookAsync(string symbol, int limit = 20, CancellationToken cancellationToken = default);
    Task SubscribeToKlinesAsync(string symbol, string interval, CancellationToken cancellationToken = default);
    Task SubscribeToMiniTickersAsync(CancellationToken cancellationToken = default);
    Task SubscribeToTickersAsync(CancellationToken cancellationToken = default);
    Task UnsubscribeAllAsync(CancellationToken cancellationToken = default);
}
