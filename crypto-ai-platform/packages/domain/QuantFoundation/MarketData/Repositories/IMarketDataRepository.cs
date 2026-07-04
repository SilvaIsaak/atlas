using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.Repositories;

public interface IMarketDataRepository
{
    Task SaveCandlesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        string timeframe, 
        IEnumerable<OhlcvData> candles, 
        CancellationToken cancellationToken = default);

    Task SaveTradesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        IEnumerable<TradeData> trades, 
        CancellationToken cancellationToken = default);

    Task SaveOrderBookAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        OrderBookSnapshot snapshot, 
        CancellationToken cancellationToken = default);

    Task SaveFundingRatesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        IEnumerable<FundingRateData> rates, 
        CancellationToken cancellationToken = default);

    Task SaveOpenInterestAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        IEnumerable<OpenInterestData> interests, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OhlcvData>> GetCandlesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        string timeframe, 
        DateTime startTime, 
        DateTime? endTime = null, 
        int limit = 1000, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TradeData>> GetTradesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        DateTime startTime, 
        DateTime? endTime = null, 
        int limit = 1000, 
        CancellationToken cancellationToken = default);
}
