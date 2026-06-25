namespace CryptoAIPlatform.Domain.Indicators;

public record RsiInput(List<decimal> Prices, int Period = 14);
public record RsiResult(List<decimal> Values);

public class RsiIndicator : ITechnicalIndicator<RsiInput, RsiResult>
{
    public RsiResult Calculate(RsiInput input)
    {
        if (input.Prices.Count < input.Period + 1)
        {
            return new RsiResult(new List<decimal>());
        }

        var rsiValues = new List<decimal>();
        var gains = new List<decimal>();
        var losses = new List<decimal>();

        for (int i = 1; i < input.Prices.Count; i++)
        {
            decimal change = input.Prices[i] - input.Prices[i - 1];
            if (change > 0)
            {
                gains.Add(change);
                losses.Add(0);
            }
            else
            {
                gains.Add(0);
                losses.Add(-change);
            }
        }

        decimal avgGain = gains.Take(input.Period).Average();
        decimal avgLoss = losses.Take(input.Period).Average();
        decimal rs = avgLoss == 0 ? 100 : avgGain / avgLoss;
        rsiValues.Add(100 - (100 / (1 + rs)));

        for (int i = input.Period; i < gains.Count; i++)
        {
            avgGain = (avgGain * (input.Period - 1) + gains[i]) / input.Period;
            avgLoss = (avgLoss * (input.Period - 1) + losses[i]) / input.Period;
            rs = avgLoss == 0 ? 100 : avgGain / avgLoss;
            rsiValues.Add(100 - (100 / (1 + rs)));
        }

        return new RsiResult(rsiValues);
    }
}
