using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.DataQuality.Rules;

public class NegativeVolumeRule : ICandleDataQualityRule
{
    private readonly ILogger<NegativeVolumeRule> _logger;

    public NegativeVolumeRule(ILogger<NegativeVolumeRule> logger)
    {
        _logger = logger;
    }

    public string RuleName => "NegativeVolumeRule";
    public string Description => "Checks for candles with negative or zero volume";

    public async Task<IEnumerable<Anomaly>> CheckAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        string timeframe,
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default)
    {
        var anomalies = new List<Anomaly>();
        foreach (var candle in candles)
        {
            if (candle.Volume <= 0)
            {
                _logger.LogWarning("Negative/zero volume candle at {Time} for {Symbol}:{Timeframe}",
                    candle.OpenTime, symbol, timeframe);
                anomalies.Add(Anomaly.Create(
                    Guid.NewGuid(),
                    tenantId,
                    Guid.Empty,
                    symbol,
                    AnomalyType.InvalidValue,
                    AnomalySeverity.High));
            }
        }

        return await Task.FromResult(anomalies);
    }
}
