using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.FeatureStore;

public class FeatureStoreService : IFeatureStoreService
{
    private readonly IEnumerable<IFeatureCalculator> _calculators;
    private readonly ILogger<FeatureStoreService> _logger;

    public FeatureStoreService(
        IEnumerable<IFeatureCalculator> calculators,
        ILogger<FeatureStoreService> logger)
    {
        _calculators = calculators;
        _logger = logger;
    }

    public async Task<IEnumerable<decimal>> CalculateFeatureAsync(
        TenantId tenantId,
        string featureName,
        string exchange,
        string symbol,
        string timeframe,
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Calculating {Feature} for {Symbol}:{Timeframe} (Tenant: {TenantId})",
            featureName, symbol, timeframe, tenantId.Value);

        var calculator = _calculators.FirstOrDefault(c => c.FeatureName.Equals(featureName, StringComparison.OrdinalIgnoreCase));
        if (calculator == null)
        {
            throw new InvalidOperationException($"No calculator found for feature {featureName}");
        }

        return await calculator.CalculateAsync(candles, cancellationToken);
    }
}
