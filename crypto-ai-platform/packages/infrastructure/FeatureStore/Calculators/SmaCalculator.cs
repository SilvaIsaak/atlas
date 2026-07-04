using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.FeatureStore.Calculators;

public class SmaCalculator : IFeatureCalculator
{
    private readonly ILogger<SmaCalculator> _logger;
    private const int DefaultPeriod = 20;

    public SmaCalculator(ILogger<SmaCalculator> logger)
    {
        _logger = logger;
    }

    public string FeatureName => "SMA";
    public string Description => "Simple Moving Average (default period 20)";

    public async Task<IEnumerable<decimal>> CalculateAsync(
        IEnumerable<OhlcvData> candles,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Calculating SMA for {Count} candles", candles.Count());
        var candleList = candles.OrderBy(c => c.OpenTime).ToList();
        var smaValues = new List<decimal>();

        decimal sum = 0;
        for (int i = 0; i < candleList.Count; i++)
        {
            sum += candleList[i].Close;
            if (i >= DefaultPeriod - 1)
            {
                smaValues.Add(sum / DefaultPeriod);
                sum -= candleList[i - DefaultPeriod + 1].Close;
            }
            else
            {
                smaValues.Add(0);
            }
        }

        return await Task.FromResult(smaValues);
    }
}
