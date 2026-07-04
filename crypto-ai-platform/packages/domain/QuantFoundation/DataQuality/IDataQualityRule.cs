using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality;

public interface IDataQualityRule
{
    string RuleName { get; }
    string Description { get; }
}

public interface ICandleDataQualityRule : IDataQualityRule
{
    Task<IEnumerable<Anomaly>> CheckAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        string timeframe,
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default);
}

public interface ITradeDataQualityRule : IDataQualityRule
{
    Task<IEnumerable<Anomaly>> CheckAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        IEnumerable<TradeData> trades,
        CancellationToken cancellationToken = default);
}
