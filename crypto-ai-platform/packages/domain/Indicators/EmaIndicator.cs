namespace CryptoAIPlatform.Domain.Indicators;

public record EmaInput(List<decimal> Prices, int Period);
public record EmaResult(List<decimal> Values);

public class EmaIndicator : ITechnicalIndicator<EmaInput, EmaResult>
{
    public EmaResult Calculate(EmaInput input)
    {
        if (input.Prices.Count < input.Period)
        {
            return new EmaResult(new List<decimal>());
        }

        var emaValues = new List<decimal>();
        decimal multiplier = 2.0m / (input.Period + 1);

        // Calculate initial SMA as first EMA
        decimal initialSma = input.Prices.Take(input.Period).Sum() / input.Period;
        emaValues.Add(initialSma);

        for (int i = input.Period; i < input.Prices.Count; i++)
        {
            decimal ema = (input.Prices[i] * multiplier) + (emaValues.Last() * (1 - multiplier));
            emaValues.Add(ema);
        }

        return new EmaResult(emaValues);
    }
}
