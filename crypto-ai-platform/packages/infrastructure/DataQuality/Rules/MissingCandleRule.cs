using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.DataQuality.Rules;

public class MissingCandleRule : ICandleDataQualityRule
{
    private readonly ILogger<MissingCandleRule> _logger;

    public MissingCandleRule(ILogger<MissingCandleRule> logger)
    {
        _logger = logger;
    }

    public string RuleName => "MissingCandleRule";
    public string Description => "Checks for missing candles in a sequence";

    public async Task<IEnumerable<Anomaly>> CheckAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        string timeframe,
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default)
    {
        var anomalies = new List<Anomaly>();
        var sorted = candles.OrderBy(c => c.OpenTime).ToList();
        if (!sorted.Any()) return anomalies;

        var expectedInterval = GetIntervalMinutes(timeframe);
        var lastTime = sorted.First().OpenTime;

        for (int i = 1; i < sorted.Count; i++)
        {
            var currentTime = sorted[i].OpenTime;
            var diffMinutes = (currentTime - lastTime).TotalMinutes;
            if (diffMinutes > expectedInterval)
            {
                var missingFrom = lastTime.AddMinutes(expectedInterval);
                var missingTo = currentTime.AddMinutes(-expectedInterval);
                _logger.LogWarning("Missing candles between {From} and {To} for {Symbol}:{Timeframe}",
                    missingFrom, missingTo, symbol, timeframe);

                anomalies.Add(Anomaly.Create(
                    Guid.NewGuid(),
                    tenantId,
                    Guid.Empty,
                    symbol,
                    AnomalyType.MissingData,
                    AnomalySeverity.Medium));
            }
            lastTime = currentTime;
        }

        return await Task.FromResult(anomalies);
    }

    private static int GetIntervalMinutes(string timeframe)
    {
        return timeframe switch
        {
            "1m" => 1,
            "5m" => 5,
            "15m" => 15,
            "1h" => 60,
            "4h" => 240,
            "1d" => 1440,
            _ => 60
        };
    }
}
