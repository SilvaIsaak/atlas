using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

public interface IFeatureStoreService
{
    Task<IEnumerable<decimal>> CalculateFeatureAsync(
        TenantId tenantId,
        string featureName,
        string exchange,
        string symbol,
        string timeframe,
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default);
}

public interface IFeatureCalculator
{
    string FeatureName { get; }
    string Description { get; }
    Task<IEnumerable<decimal>> CalculateAsync(
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default);
}
