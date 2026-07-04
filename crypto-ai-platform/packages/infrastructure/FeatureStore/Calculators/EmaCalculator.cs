using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.FeatureStore.Calculators;

public class EmaCalculator : IFeatureCalculator
{
    private readonly ILogger<EmaCalculator> _logger;
    private const int DefaultPeriod = 20;

    public EmaCalculator(ILogger<EmaCalculator> logger)
    {
        _logger = logger;
    }

    public string FeatureName => "EMA";
    public string Description => "Exponential Moving Average (default period 20)";

    public async Task<IEnumerable<decimal>> CalculateAsync(
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Calculating EMA for {Count} candles", candles.Count());
        var candleList = candles.OrderBy(c => c.OpenTime).ToList();
        var emaValues = new List<decimal>();

        decimal multiplier = 2m / (DefaultPeriod + 1);
        decimal ema = 0;
        for (int i = 0; i < candleList.Count; i++)
        {
            if (i < DefaultPeriod - 1)
            {
                emaValues.Add(0);
            }
            else if (i == DefaultPeriod - 1)
            {
                decimal initialSum = 0;
                for (int j = 0; j < DefaultPeriod; j++)
                {
                    initialSum += candleList[j].Close;
                }
                ema = initialSum / DefaultPeriod;
                emaValues.Add(ema);
            }
            else
            {
                ema = candleList[i].Close * multiplier + ema * (1 - multiplier);
                emaValues.Add(ema);
            }
        }

        return await Task.FromResult(emaValues);
    }
}
