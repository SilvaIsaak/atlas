namespace CryptoAIPlatform.Domain.Indicators;

public record MacdInput(List<decimal> Prices, int FastPeriod = 12, int SlowPeriod = 26, int SignalPeriod = 9);
public record MacdResult(List<decimal> MacdLine, List<decimal> SignalLine, List<decimal> Histogram);

public class MacdIndicator : ITechnicalIndicator<MacdInput, MacdResult>
{
    public MacdResult Calculate(MacdInput input)
    {
        var emaFast = new EmaIndicator().Calculate(new EmaInput(input.Prices, input.FastPeriod)).Values;
        var emaSlow = new EmaIndicator().Calculate(new EmaInput(input.Prices, input.SlowPeriod)).Values;

        if (emaFast.Count == 0 || emaSlow.Count == 0)
        {
            return new MacdResult(new List<decimal>(), new List<decimal>(), new List<decimal>());
        }

        // Adjust lengths to match
        int diff = emaFast.Count - emaSlow.Count;
        var macdLine = new List<decimal>();
        for (int i = 0; i < emaSlow.Count; i++)
        {
            macdLine.Add(emaFast[i + diff] - emaSlow[i]);
        }

        var signalLine = new EmaIndicator().Calculate(new EmaInput(macdLine, input.SignalPeriod)).Values;

        var histogram = new List<decimal>();
        int signalStart = macdLine.Count - signalLine.Count;
        for (int i = 0; i < signalLine.Count; i++)
        {
            histogram.Add(macdLine[i + signalStart] - signalLine[i]);
        }

        return new MacdResult(macdLine, signalLine, histogram);
    }
}
