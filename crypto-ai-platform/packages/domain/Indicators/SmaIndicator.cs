namespace CryptoAIPlatform.Domain.Indicators;

public class SmaIndicator : ITechnicalIndicator<SmaInput, SmaResult>
{
    public SmaResult Calculate(SmaInput input)
    {
        if (input.Prices.Count < input.Period)
        {
            return new SmaResult(new List<decimal>());
        }

        var smaValues = new List<decimal>();

        for (int i = input.Period - 1; i < input.Prices.Count; i++)
        {
            decimal sum = 0;
            for (int j = 0; j < input.Period; j++)
            {
                sum += input.Prices[i - j];
            }

            smaValues.Add(sum / input.Period);
        }

        return new SmaResult(smaValues);
    }
}
