using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class MarketDataRepository : BaseRepository<Candle, Guid>, IMarketDataRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MarketDataRepository> _logger;

    public MarketDataRepository(
        ApplicationDbContext context, 
        ILogger<MarketDataRepository> logger)
        : base(context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SaveCandlesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        string timeframe, 
        IEnumerable<OhlcvData> candles, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var existingCandles = await _context.Candles
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId
                    && x.Exchange == exchange
                    && x.Symbol == symbol
                    && x.Timeframe == timeframe
                    && candles.Select(c => c.OpenTime).Contains(x.TimestampUtc))
                .ToDictionaryAsync(x => x.TimestampUtc, cancellationToken);

            var newCandles = new List<Candle>();
            foreach (var candleData in candles)
            {
                if (!existingCandles.ContainsKey(candleData.OpenTime))
                {
                    newCandles.Add(new Candle
                    {
                        TenantId = tenantId,
                        Exchange = exchange,
                        Symbol = symbol,
                        Timeframe = timeframe,
                        TimestampUtc = candleData.OpenTime,
                        Open = candleData.Open,
                        High = candleData.High,
                        Low = candleData.Low,
                        Close = candleData.Close,
                        Volume = candleData.Volume,
                        QuoteVolume = candleData.QuoteVolume,
                        TradeCount = candleData.TradeCount
                    });
                }
            }

            if (newCandles.Any())
            {
                await _context.Candles.AddRangeAsync(newCandles, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Saved {newCandles.Count} new candles for {exchange}:{symbol}:{timeframe} (Tenant: {tenantId.Value})");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving candles");
            throw;
        }
    }

    public async Task SaveTradesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        IEnumerable<TradeData> trades, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var existingTradeIds = await _context.Trades
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId
                    && x.Exchange == exchange
                    && x.Symbol == symbol
                    && trades.Select(t => t.TradeId).Contains(x.ExchangeTradeId))
                .Select(x => x.ExchangeTradeId)
                .ToHashSetAsync(cancellationToken);

            var newTrades = new List<Trade>();
            foreach (var tradeData in trades)
            {
                if (!existingTradeIds.Contains(tradeData.TradeId))
                {
                    newTrades.Add(new Trade
                    {
                        TenantId = tenantId,
                        Exchange = exchange,
                        Symbol = symbol,
                        TimestampUtc = tradeData.Timestamp,
                        ExchangeTradeId = tradeData.TradeId,
                        Price = tradeData.Price,
                        Quantity = tradeData.Quantity,
                        IsBuyerMaker = tradeData.IsBuyerMaker
                    });
                }
            }

            if (newTrades.Any())
            {
                await _context.Trades.AddRangeAsync(newTrades, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Saved {newTrades.Count} new trades for {exchange}:{symbol} (Tenant: {tenantId.Value})");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving trades");
            throw;
        }
    }

    public async Task SaveOrderBookAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        OrderBookSnapshot snapshot, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var orderBook = new OrderBook
            {
                TenantId = tenantId,
                Exchange = exchange,
                Symbol = symbol,
                TimestampUtc = snapshot.Timestamp,
                Bids = snapshot.Bids.ToList(),
                Asks = snapshot.Asks.ToList()
            };

            await _context.OrderBooks.AddAsync(orderBook, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Saved order book for {exchange}:{symbol} (Tenant: {tenantId.Value})");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving order book");
            throw;
        }
    }

    public async Task SaveFundingRatesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        IEnumerable<FundingRateData> rates, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var newRates = rates.Select(r => new FundingRate
            {
                TenantId = tenantId,
                Exchange = exchange,
                Symbol = symbol,
                TimestampUtc = DateTime.UtcNow,
                FundingTimeUtc = r.FundingTime,
                Rate = r.Rate
            }).ToList();

            await _context.FundingRates.AddRangeAsync(newRates, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Saved {newRates.Count} funding rates for {exchange}:{symbol} (Tenant: {tenantId.Value})");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving funding rates");
            throw;
        }
    }

    public async Task SaveOpenInterestAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        IEnumerable<OpenInterestData> interests, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var newInterests = interests.Select(i => new OpenInterest
            {
                TenantId = tenantId,
                Exchange = exchange,
                Symbol = symbol,
                TimestampUtc = i.Timestamp,
                Interest = i.Interest
            }).ToList();

            await _context.OpenInterests.AddRangeAsync(newInterests, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Saved {newInterests.Count} open interest records for {exchange}:{symbol} (Tenant: {tenantId.Value})");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving open interest");
            throw;
        }
    }

    public async Task<IReadOnlyList<OhlcvData>> GetCandlesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        string timeframe, 
        DateTime startTime, 
        DateTime? endTime = null, 
        int limit = 1000, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Candles
            .AsNoTracking()
            .Where(x => x.TenantId == tenantId
                && x.Exchange == exchange
                && x.Symbol == symbol
                && x.Timeframe == timeframe
                && x.TimestampUtc >= startTime);

        if (endTime.HasValue)
        {
            query = query.Where(x => x.TimestampUtc <= endTime.Value);
        }

        var candles = await query
            .OrderBy(x => x.TimestampUtc)
            .Take(limit)
            .Select(x => new OhlcvData
            {
                OpenTime = x.TimestampUtc,
                Open = x.Open,
                High = x.High,
                Low = x.Low,
                Close = x.Close,
                Volume = x.Volume,
                CloseTime = x.TimestampUtc.AddMinutes(1),
                QuoteVolume = x.QuoteVolume ?? 0,
                TradeCount = x.TradeCount
            })
            .ToListAsync(cancellationToken);

        return candles.AsReadOnly();
    }

    public async Task<IReadOnlyList<TradeData>> GetTradesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        DateTime startTime, 
        DateTime? endTime = null, 
        int limit = 1000, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Trades
            .AsNoTracking()
            .Where(x => x.TenantId == tenantId
                && x.Exchange == exchange
                && x.Symbol == symbol
                && x.TimestampUtc >= startTime);

        if (endTime.HasValue)
        {
            query = query.Where(x => x.TimestampUtc <= endTime.Value);
        }

        var trades = await query
            .OrderBy(x => x.TimestampUtc)
            .Take(limit)
            .Select(x => new TradeData
            {
                TradeId = x.ExchangeTradeId,
                Price = x.Price,
                Quantity = x.Quantity,
                Timestamp = x.TimestampUtc,
                IsBuyerMaker = x.IsBuyerMaker
            })
            .ToListAsync(cancellationToken);

        return trades.AsReadOnly();
    }
}
