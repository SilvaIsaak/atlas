using CryptoExchange.Net.Authentication;
using Binance.Net.Clients;
using Binance.Net.Enums;
using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Infrastructure.Exchanges;

public class BinanceClient : IExchangeClient
{
    private readonly BinanceRestClient _restClient;

    public BinanceClient(string apiKey, string apiSecret)
    {
        _restClient = new BinanceRestClient(options =>
        {
            options.ApiCredentials = new ApiCredentials(apiKey, apiSecret);
        });
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
            BidPrice = ticker.BidPrice,
            AskPrice = ticker.AskPrice,
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
            "1m" => KlineInterval.OneMinute,
            "5m" => KlineInterval.FiveMinutes,
            "15m" => KlineInterval.FifteenMinutes,
            "1h" => KlineInterval.OneHour,
            "4h" => KlineInterval.FourHours,
            "1d" => KlineInterval.OneDay,
            _ => KlineInterval.OneHour
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

    public async Task<ExchangeOrder> PlaceOrderAsync(PlaceOrderRequest request, CancellationToken cancellationToken = default)
    {
        var side = request.Side == OrderSide.Buy ? Binance.Net.Enums.OrderSide.Buy : Binance.Net.Enums.OrderSide.Sell;
        var type = request.Type switch
        {
            OrderType.Market => Binance.Net.Enums.OrderType.Market,
            OrderType.Limit => Binance.Net.Enums.OrderType.Limit,
            OrderType.StopLoss => Binance.Net.Enums.OrderType.StopLoss,
            OrderType.StopLossLimit => Binance.Net.Enums.OrderType.StopLossLimit,
            OrderType.TakeProfit => Binance.Net.Enums.OrderType.TakeProfit,
            OrderType.TakeProfitLimit => Binance.Net.Enums.OrderType.TakeProfitLimit,
            _ => Binance.Net.Enums.OrderType.Market
        };

        var result = await _restClient.SpotApi.Trading.PlaceOrderAsync(
            request.Symbol,
            side,
            type,
            Binance.Net.Enums.TimeInForce.GoodTillCanceled,
            request.Quantity,
            request.Price,
            stopPrice: request.StopPrice,
            cancellationToken: cancellationToken
        );

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
        var result = await _restClient.SpotApi.Trading.GetOrderAsync(null, long.Parse(orderId), cancellationToken: cancellationToken);
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
        var result = await _restClient.SpotApi.Trading.CancelOrderAsync(symbol, long.Parse(orderId), cancellationToken: cancellationToken);
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

    public async Task<List<ExchangeBalance>> GetBalancesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.SpotApi.Account.GetAccountInfoAsync(cancellationToken: cancellationToken);
        if (!result.Success) throw new InvalidOperationException($"Binance error: {result.Error}");

        var balances = result.Data.Balances;
        return balances.Select(b => new ExchangeBalance
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

    private static OrderType MapOrderType(Binance.Net.Enums.OrderType type) => type switch
    {
        Binance.Net.Enums.OrderType.Market => OrderType.Market,
        Binance.Net.Enums.OrderType.Limit => OrderType.Limit,
        Binance.Net.Enums.OrderType.StopLoss => OrderType.StopLoss,
        Binance.Net.Enums.OrderType.StopLossLimit => OrderType.StopLossLimit,
        Binance.Net.Enums.OrderType.TakeProfit => OrderType.TakeProfit,
        Binance.Net.Enums.OrderType.TakeProfitLimit => OrderType.TakeProfitLimit,
        _ => OrderType.Market
    };
}
