namespace CryptoAIPlatform.Domain.Indicators;

public interface ITechnicalIndicator<TInput, TResult>
{
    TResult Calculate(TInput input);
}

public record SmaInput(List<decimal> Prices, int Period);
public record SmaResult(List<decimal> Values);
