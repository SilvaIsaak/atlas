using CryptoExchange.Net.Authentication;
using Binance.Net.Clients;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.Exchanges;

public class BinanceSpotConnector : IExchangeConnector
{
    private readonly BinanceRestClient _restClient;
    private readonly BinanceSocketClient _socketClient;
    private readonly ILogger<BinanceSpotConnector> _logger;

    public BinanceSpotConnector(
        BinanceOptions options,
        ILogger<BinanceSpotConnector> logger)
    {
        _restClient = new BinanceRestClient(opts =>
        {
            opts.ApiCredentials = new ApiCredentials(options.ApiKey, options.ApiSecret);
        });
        _socketClient = new BinanceSocketClient(opts =>
        {
            opts.ApiCredentials = new ApiCredentials(options.ApiKey, options.ApiSecret);
        });
        _logger = logger;
        MarketDataService = new BinanceSpotMarketDataService(_restClient, _logger);
        TradingService = new BinanceSpotTradingService(_restClient, _logger);
        AuthenticationService = new BinanceSpotAuthenticationService(_restClient, _logger);
        HealthService = new BinanceSpotHealthService(_restClient, _logger);
    }

    public IExchangeMarketDataService MarketDataService { get; }
    public IExchangeTradingService TradingService { get; }
    public IExchangeAuthenticationService AuthenticationService { get; }
    public IExchangeHealthService HealthService { get; }
}

public class BinanceSpotMarketDataService : IExchangeMarketDataService
{
    private readonly BinanceRestClient _restClient;
    private readonly ILogger _logger;

    public BinanceSpotMarketDataService(
        BinanceRestClient restClient,
        ILogger logger)
    {
        _restClient = restClient;
        _logger = logger;
    }

    public async Task<ExchangeTicker> GetTickerAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetTickerAsync(symbol, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        var ticker = result.Data;
        return new ExchangeTicker
        {
            Symbol = ticker.Symbol,
            LastPrice = ticker.LastPrice,
            BidPrice = ticker.BestBidPrice,
            AskPrice = ticker.BestAskPrice,
            Volume24h = ticker.Volume
        };
    }

    public async Task<ExchangeOrderBook> GetOrderBookAsync(string symbol, int limit = 20, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetOrderBookAsync(symbol, limit, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        var orderBook = result.Data;
        return new ExchangeOrderBook
        {
            Symbol = symbol,
            Bids = orderBook.Bids.Select(b => new OrderBookEntry
            {
                Price = b.Price,
                Quantity = b.Quantity
            }).ToList(),
            Asks = orderBook.Asks.Select(a => new OrderBookEntry
            {
                Price = a.Price,
                Quantity = a.Quantity
            }).ToList()
        };
    }

    public async Task<List<ExchangeKline>> GetKlinesAsync(string symbol, string interval, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default)
    {
        var binanceInterval = interval switch
        {
            "1m" => Binance.Net.Enums.KlineInterval.OneMinute,
            "5m" => Binance.Net.Enums.KlineInterval.FiveMinutes,
            "15m" => Binance.Net.Enums.KlineInterval.FifteenMinutes,
            "1h" => Binance.Net.Enums.KlineInterval.OneHour,
            "4h" => Binance.Net.Enums.KlineInterval.FourHour,
            "1d" => Binance.Net.Enums.KlineInterval.OneDay,
            _ => Binance.Net.Enums.KlineInterval.OneHour
        };
        var result = await _restClient.SpotApi.ExchangeData.GetKlinesAsync(symbol, binanceInterval, startTime, endTime, null, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        return result.Data.Select(k => new ExchangeKline
        {
            OpenTime = k.OpenTime,
            Open = k.OpenPrice,
            High = k.HighPrice,
            Low = k.LowPrice,
            Close = k.ClosePrice,
            Volume = k.Volume,
            CloseTime = k.CloseTime
        }).ToList();
    }

    public async Task<ExchangeInfo> GetExchangeInfoAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetExchangeInfoAsync((string?)null, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        var data = result.Data;
        return new ExchangeInfo
        {
            Timezone = 0,
            ServerTime = new DateTimeOffset(data.ServerTime).ToUnixTimeMilliseconds(),
                RateLimits = data.RateLimits.Select(l => new ExchangeRateLimit
                {
                    RateLimitType = l.Type.ToString(),
                    Interval = l.Interval.ToString(),
                    IntervalNum = l.IntervalNumber,
                    Limit = l.Limit
                }).ToList(),
            Symbols = data.Symbols.Select(s => new ExchangeSymbol
            {
                Name = s.Name,
                Status = s.Status.ToString(),
                BaseAsset = s.BaseAsset,
                BaseAssetPrecision = s.BaseAssetPrecision,
                QuoteAsset = s.QuoteAsset,
                QuotePrecision = s.QuoteAssetPrecision,
                OrderTypes = s.OrderTypes.Select(t => t.ToString()).ToList()
            }).ToList()
        };
    }

    public async Task<DateTime> GetServerTimeAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetServerTimeAsync(cancellationToken);
        return result.Data;
    }

    public async Task<bool> PingAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.PingAsync(cancellationToken);
        return result.Success;
    }

    public async Task<List<ExchangeSymbol>> GetSymbolsAsync(CancellationToken cancellationToken = default)
    {
        var info = await GetExchangeInfoAsync(cancellationToken);
        return info.Symbols;
    }

    public async Task<List<ExchangeTicker>> GetTickersAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetTickersAsync(cancellationToken);
        return result.Data.Select(t => new ExchangeTicker
        {
            Symbol = t.Symbol,
            LastPrice = t.LastPrice,
            BidPrice = t.BestBidPrice,
            AskPrice = t.BestAskPrice,
            Volume24h = t.Volume
        }).ToList();
    }

    public async Task<List<ExchangeTrade>> GetRecentTradesAsync(string symbol, int limit = 500, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetRecentTradesAsync(symbol, limit, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        return result.Data.Select(t => new ExchangeTrade
        {
            Id = t.OrderId,
            Price = t.Price,
            Quantity = t.BaseQuantity,
            Time = t.TradeTime,
            IsBuyerMaker = t.BuyerIsMaker
        }).ToList();
    }

    public async Task<List<ExchangeTrade>> GetHistoricalTradesAsync(string symbol, int limit = 500, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetRecentTradesAsync(symbol, limit, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        return result.Data.Select(t => new ExchangeTrade
        {
            Id = t.OrderId,
            Price = t.Price,
            Quantity = t.BaseQuantity,
            Time = t.TradeTime,
            IsBuyerMaker = t.BuyerIsMaker
        }).ToList();
    }

    public async Task<Exchange24hStatistics> Get24hStatisticsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.ExchangeData.GetTradingDayTickerAsync(symbol, null, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        var data = result.Data;
        return new Exchange24hStatistics
        {
            Symbol = data.Symbol,
            PriceChange = data.PriceChange,
            PriceChangePercent = data.PriceChangePercent,
            LastPrice = data.LastPrice,
            OpenPrice = data.OpenPrice,
            HighPrice = data.HighPrice,
            LowPrice = data.LowPrice,
            Volume = data.Volume,
            QuoteVolume = data.QuoteVolume,
            OpenTime = data.OpenTime,
            CloseTime = data.CloseTime,
            FirstTradeId = data.FirstTradeId,
            LastTradeId = data.LastTradeId,
            TradeCount = (int)data.TotalTrades
        };
    }

    public Task<FundingRateData> GetFundingRateAsync(string symbol, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Funding Rate not available on Binance Spot");
    }

    public Task<OpenInterestData> GetOpenInterestAsync(string symbol, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Open Interest not available on Binance Spot");
    }

    public Task<MarkPriceData> GetMarkPriceAsync(string symbol, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Mark Price not available on Binance Spot");
    }

    public Task<IndexPriceData> GetIndexPriceAsync(string symbol, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Index Price not available on Binance Spot");
    }
}

public class BinanceSpotTradingService : IExchangeTradingService
{
    private readonly BinanceRestClient _restClient;
    private readonly ILogger _logger;

    public BinanceSpotTradingService(
        BinanceRestClient restClient,
        ILogger logger)
    {
        _restClient = restClient;
        _logger = logger;
    }

    public async Task<ExchangeOrder> PlaceOrderAsync(PlaceOrderRequest request, CancellationToken cancellationToken = default)
    {
        var side = request.Side == OrderSide.Buy ? Binance.Net.Enums.OrderSide.Buy : Binance.Net.Enums.OrderSide.Sell;
        var type = request.Type switch
        {
            OrderType.Market => Binance.Net.Enums.SpotOrderType.Market,
            OrderType.Limit => Binance.Net.Enums.SpotOrderType.Limit,
            OrderType.StopLoss => Binance.Net.Enums.SpotOrderType.StopLoss,
            OrderType.StopLossLimit => Binance.Net.Enums.SpotOrderType.StopLossLimit,
            OrderType.TakeProfit => Binance.Net.Enums.SpotOrderType.TakeProfit,
            OrderType.TakeProfitLimit => Binance.Net.Enums.SpotOrderType.TakeProfitLimit,
            _ => Binance.Net.Enums.SpotOrderType.Market
        };

        var result = await _restClient.SpotApi.Trading.PlaceOrderAsync(
            request.Symbol,
            side,
            type,
            request.Quantity,
            null,
            null,
            request.Price,
            Binance.Net.Enums.TimeInForce.GoodTillCanceled,
            request.StopPrice,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            cancellationToken);

        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        var order = result.Data;

        return new ExchangeOrder
        {
            ExchangeOrderId = order.Id.ToString(),
            Symbol = order.Symbol,
            Side = request.Side,
            Type = request.Type,
            Quantity = order.Quantity,
            Price = order.Price,
            Status = MapStatus(order.Status),
            CreatedAt = order.CreateTime,
            ExecutedQuantity = order.QuantityFilled,
            CumulativeQuoteQuantity = order.QuoteQuantityFilled
        };
    }

    public async Task<ExchangeOrder> GetOrderAsync(string orderId, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.Trading.GetOrderAsync((string?)null, long.Parse(orderId), (string?)null, (long?)null, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        var order = result.Data;
        return new ExchangeOrder
        {
            ExchangeOrderId = order.Id.ToString(),
            Symbol = order.Symbol,
            Side = order.Side == Binance.Net.Enums.OrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
            Type = MapOrderType(order.Type),
            Quantity = order.Quantity,
            Price = order.Price,
            Status = MapStatus(order.Status),
            CreatedAt = order.CreateTime,
            ExecutedQuantity = order.QuantityFilled,
            CumulativeQuoteQuantity = order.QuoteQuantityFilled
        };
    }

    public async Task<ExchangeOrder> CancelOrderAsync(string orderId, string symbol, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.Trading.CancelOrderAsync(symbol, long.Parse(orderId), (string?)null, (string?)null, (Binance.Net.Enums.CancelRestriction?)null, (long?)null, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        var order = result.Data;
        return new ExchangeOrder
        {
            ExchangeOrderId = order.Id.ToString(),
            Symbol = order.Symbol,
            Side = order.Side == Binance.Net.Enums.OrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
            Type = MapOrderType(order.Type),
            Quantity = order.Quantity,
            Price = order.Price,
            Status = MapStatus(order.Status),
            CreatedAt = order.CreateTime,
            ExecutedQuantity = order.QuantityFilled,
            CumulativeQuoteQuantity = order.QuoteQuantityFilled
        };
    }

    public async Task<List<ExchangeOrder>> GetOpenOrdersAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.Trading.GetOpenOrdersAsync(symbol, null, cancellationToken);
        return result.Data.Select(order => new ExchangeOrder
        {
            ExchangeOrderId = order.Id.ToString(),
            Symbol = order.Symbol,
            Side = order.Side == Binance.Net.Enums.OrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
            Type = MapOrderType(order.Type),
            Quantity = order.Quantity,
            Price = order.Price,
            Status = MapStatus(order.Status),
            CreatedAt = order.CreateTime,
            ExecutedQuantity = order.QuantityFilled,
            CumulativeQuoteQuantity = order.QuoteQuantityFilled
        }).ToList();
    }

    public async Task<List<ExchangeOrder>> GetOrderHistoryAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.Trading.GetOrdersAsync(symbol, null, null, null, null, null, cancellationToken);
        return result.Data.Select(order => new ExchangeOrder
        {
            ExchangeOrderId = order.Id.ToString(),
            Symbol = order.Symbol,
            Side = order.Side == Binance.Net.Enums.OrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
            Type = MapOrderType(order.Type),
            Quantity = order.Quantity,
            Price = order.Price,
            Status = MapStatus(order.Status),
            CreatedAt = order.CreateTime,
            ExecutedQuantity = order.QuantityFilled,
            CumulativeQuoteQuantity = order.QuoteQuantityFilled
        }).ToList();
    }

    public async Task<List<ExchangeBalance>> GetBalancesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.Account.GetAccountInfoAsync(null, null, cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");
        return result.Data.Balances.Select(b => new ExchangeBalance
        {
            Asset = b.Asset,
            Free = b.Available,
            Locked = b.Locked,
            Total = b.Total
        }).Where(b => b.Total > 0).ToList();
    }

    private static OrderStatus MapStatus(Binance.Net.Enums.OrderStatus status) => status switch
    {
        Binance.Net.Enums.OrderStatus.New => OrderStatus.New,
        Binance.Net.Enums.OrderStatus.PartiallyFilled => OrderStatus.PartiallyFilled,
        Binance.Net.Enums.OrderStatus.Filled => OrderStatus.Filled,
        Binance.Net.Enums.OrderStatus.Canceled => OrderStatus.Cancelled,
        Binance.Net.Enums.OrderStatus.PendingCancel => OrderStatus.PendingCancel,
        Binance.Net.Enums.OrderStatus.Rejected => OrderStatus.Rejected,
        Binance.Net.Enums.OrderStatus.Expired => OrderStatus.Expired,
        _ => OrderStatus.New
    };

    private static OrderType MapOrderType(Binance.Net.Enums.SpotOrderType type) => type switch
    {
        Binance.Net.Enums.SpotOrderType.Market => OrderType.Market,
        Binance.Net.Enums.SpotOrderType.Limit => OrderType.Limit,
        Binance.Net.Enums.SpotOrderType.StopLoss => OrderType.StopLoss,
        Binance.Net.Enums.SpotOrderType.StopLossLimit => OrderType.StopLossLimit,
        Binance.Net.Enums.SpotOrderType.TakeProfit => OrderType.TakeProfit,
        Binance.Net.Enums.SpotOrderType.TakeProfitLimit => OrderType.TakeProfitLimit,
        _ => OrderType.Market
    };
}

public class BinanceSpotAuthenticationService : IExchangeAuthenticationService
{
    private readonly BinanceRestClient _restClient;
    private readonly ILogger _logger;

    public BinanceSpotAuthenticationService(
        BinanceRestClient restClient,
        ILogger logger)
    {
        _restClient = restClient;
        _logger = logger;
    }

    public async Task<bool> ValidateCredentialsAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.Account.GetAccountInfoAsync(null, null, cancellationToken);
        return result.Success;
    }
}

public class BinanceSpotHealthService : IExchangeHealthService
{
    private readonly BinanceRestClient _restClient;
    private readonly ILogger _logger;

    public BinanceSpotHealthService(
        BinanceRestClient restClient,
        ILogger logger)
    {
        _restClient = restClient;
        _logger = logger;
    }

    public async Task<bool> CheckRestHealthAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _restClient.SpotApi.ExchangeData.PingAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Binance REST health check failed");
            return false;
        }
    }

    public Task<bool> CheckWebSocketHealthAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public async Task<TimeSpan> GetLatencyAsync(CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        await _restClient.SpotApi.ExchangeData.PingAsync(cancellationToken);
        return DateTime.UtcNow - startTime;
    }
}
