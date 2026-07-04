using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public interface IMarketDataService
{
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

    Task<OrderBookSnapshot> GetOrderBookAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        int limit = 20, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FundingRateData>> GetFundingRatesAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        DateTime startTime, 
        DateTime? endTime = null, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OpenInterestData>> GetOpenInterestAsync(
        TenantId tenantId, 
        string exchange, 
        string symbol, 
        DateTime startTime, 
        DateTime? endTime = null, 
        CancellationToken cancellationToken = default);
}
