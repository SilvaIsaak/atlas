namespace CryptoAIPlatform.Domain.Exchanges;

public interface IExchangeTradingService
{
    Task<ExchangeOrder> PlaceOrderAsync(PlaceOrderRequest request, CancellationToken cancellationToken = default);
    Task<ExchangeOrder> GetOrderAsync(string orderId, CancellationToken cancellationToken = default);
    Task<ExchangeOrder> CancelOrderAsync(string orderId, string symbol, CancellationToken cancellationToken = default);
    Task<List<ExchangeOrder>> GetOpenOrdersAsync(string symbol, CancellationToken cancellationToken = default);
    Task<List<ExchangeOrder>> GetOrderHistoryAsync(string symbol, CancellationToken cancellationToken = default);
    Task<List<ExchangeBalance>> GetBalancesAsync(CancellationToken cancellationToken = default);
}
