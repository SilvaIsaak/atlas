using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.DataQuality;

public class DataQualityService : IDataQualityService
{
    private readonly IEnumerable<ICandleDataQualityRule> _candleRules;
    private readonly IEnumerable<ITradeDataQualityRule> _tradeRules;
    private readonly ILogger<DataQualityService> _logger;

    public DataQualityService(
        IEnumerable<ICandleDataQualityRule> candleRules,
        IEnumerable<ITradeDataQualityRule> tradeRules,
        ILogger<DataQualityService> logger)
    {
        _candleRules = candleRules;
        _tradeRules = tradeRules;
        _logger = logger;
    }

    public async Task<DataQualityResult> ValidateCandlesAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        string timeframe,
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default)
    {
        var result = new DataQualityResult();
        var candleList = candles.ToList();
        result.TotalRecordsChecked = candleList.Count;

        _logger.LogInformation("Starting validation for {Count} candles for {Symbol}:{Timeframe} (Tenant: {TenantId})",
            candleList.Count, symbol, timeframe, tenantId.Value);

        foreach (var rule in _candleRules)
        {
            try
            {
                var anomalies = await rule.CheckAsync(tenantId, exchange, symbol, timeframe, candleList, cancellationToken);
                result.Anomalies.AddRange(anomalies);
                if (anomalies.Any())
                {
                    result.FailedRules.Add(new FailedRule
                    {
                        RuleName = rule.RuleName,
                        Description = rule.Description,
                        AffectedRecords = anomalies.Select(a => a.Id.ToString()).ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing rule {Rule}", rule.RuleName);
                result.FailedRules.Add(new FailedRule
                {
                    RuleName = rule.RuleName,
                    Description = $"Error: {ex.Message}",
                    AffectedRecords = new List<string>()
                });
            }
        }

        _logger.LogInformation("Validation completed for {Symbol}:{Timeframe} (Valid: {IsValid}, Anomalies: {AnomalyCount})",
            symbol, timeframe, result.IsValid, result.Anomalies.Count);

        return result;
    }

    public async Task<DataQualityResult> ValidateTradesAsync(
        TenantId tenantId,
        string exchange,
        string symbol,
        IEnumerable<TradeData> trades,
        CancellationToken cancellationToken = default)
    {
        var result = new DataQualityResult();
        var tradeList = trades.ToList();
        result.TotalRecordsChecked = tradeList.Count;
        return await Task.FromResult(result);
    }
}
