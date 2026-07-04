using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality;

public interface IDataQualityService
{
    Task<DataQualityResult> ValidateCandlesAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        string timeframe,
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default);

    Task<DataQualityResult> ValidateTradesAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        IEnumerable<TradeData> trades,
        CancellationToken cancellationToken = default);
}

public record DataQualityResult
{
    public bool IsValid => FailedRules.Count == 0;
    public int TotalRecordsChecked { get; init; }
    public List<FailedRule> FailedRules { get; init; } = new();
    public List<Anomaly> Anomalies { get; init; } = new();
}

public record FailedRule
{
    public string RuleName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<string> AffectedRecords { get; init; } = new();
}
