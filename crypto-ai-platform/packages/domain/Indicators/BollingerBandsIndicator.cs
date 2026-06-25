namespace CryptoAIPlatform.Domain.Indicators;

public record BollingerBandsInput(List<decimal> Prices, int Period = 20, decimal StdDevMultiplier = 2);
public record BollingerBandsResult(List<decimal> UpperBand, List<decimal> MiddleBand, List<decimal> LowerBand);

public class BollingerBandsIndicator : ITechnicalIndicator<BollingerBandsInput, BollingerBandsResult>
{
    public BollingerBandsResult Calculate(BollingerBandsInput input)
    {
        var sma = new SmaIndicator().Calculate(new SmaInput(input.Prices, input.Period)).Values;
        if (sma.Count == 0)
        {
            return new BollingerBandsResult(new List<decimal>(), new List<decimal>(), new List<decimal>());
        }

        var upperBand = new List<decimal>();
        var lowerBand = new List<decimal>();

        for (int i = input.Period - 1; i < input.Prices.Count; i++)
        {
            var window = input.Prices.Skip(i - input.Period + 1).Take(input.Period).ToList();
            decimal mean = window.Average();
            decimal stdDev = (decimal)Math.Sqrt((double)window.Average(x => Math.Pow((double)(x - mean), 2)));
            int smaIndex = i - input.Period + 1;
            
            upperBand.Add(sma[smaIndex] + input.StdDevMultiplier * stdDev);
            lowerBand.Add(sma[smaIndex] - input.StdDevMultiplier * stdDev);
        }

        return new BollingerBandsResult(upperBand, sma, lowerBand);
    }
}
