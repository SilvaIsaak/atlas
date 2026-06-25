namespace CryptoAIPlatform.Domain.Exchanges;

public interface IExchangeClient
{
    Task<ExchangeTicker> GetTickerAsync(string symbol, CancellationToken cancellationToken = default);
    Task<ExchangeOrderBook> GetOrderBookAsync(string symbol, int limit = 20, CancellationToken cancellationToken = default);
    Task<List<ExchangeKline>> GetKlinesAsync(string symbol, string interval, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default);
    Task<ExchangeOrder> PlaceOrderAsync(PlaceOrderRequest request, CancellationToken cancellationToken = default);
    Task<ExchangeOrder> GetOrderAsync(string orderId, CancellationToken cancellationToken = default);
    Task<ExchangeOrder> CancelOrderAsync(string orderId, string symbol, CancellationToken cancellationToken = default);
    Task<List<ExchangeBalance>> GetBalancesAsync(CancellationToken cancellationToken = default);
}

public record ExchangeTicker
{
    public string Symbol { get; init; } = string.Empty;
    public decimal LastPrice { get; init; }
    public decimal BidPrice { get; init; }
    public decimal AskPrice { get; init; }
    public decimal Volume24h { get; init; }
}

public record ExchangeOrderBook
{
    public string Symbol { get; init; } = string.Empty;
    public List<OrderBookEntry> Bids { get; init; } = new();
    public List<OrderBookEntry> Asks { get; init; } = new();
}

public record OrderBookEntry
{
    public decimal Price { get; init; }
    public decimal Quantity { get; init; }
}

public record ExchangeKline
{
    public DateTime OpenTime { get; init; }
    public decimal Open { get; init; }
    public decimal High { get; init; }
    public decimal Low { get; init; }
    public decimal Close { get; init; }
    public decimal Volume { get; init; }
    public DateTime CloseTime { get; init; }
}

public record ExchangeOrder
{
    public string ExchangeOrderId { get; init; } = string.Empty;
    public string Symbol { get; init; } = string.Empty;
    public OrderSide Side { get; init; }
    public OrderType Type { get; init; }
    public decimal Quantity { get; init; }
    public decimal? Price { get; init; }
    public OrderStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public decimal? ExecutedQuantity { get; init; }
    public decimal? CumulativeQuoteQuantity { get; init; }
}

public enum OrderSide
{
    Buy,
    Sell
}

public enum OrderType
{
    Market,
    Limit,
    StopLoss,
    StopLossLimit,
    TakeProfit,
    TakeProfitLimit
}

public enum OrderStatus
{
    New,
    PartiallyFilled,
    Filled,
    Cancelled,
    PendingCancel,
    Rejected,
    Expired
}

public record PlaceOrderRequest
{
    public string Symbol { get; init; } = string.Empty;
    public OrderSide Side { get; init; }
    public OrderType Type { get; init; }
    public decimal Quantity { get; init; }
    public decimal? Price { get; init; }
    public decimal? StopPrice { get; init; }
}

public record ExchangeBalance
{
    public string Asset { get; init; } = string.Empty;
    public decimal Free { get; init; }
    public decimal Locked { get; init; }
    public decimal Total { get; init; }
}
