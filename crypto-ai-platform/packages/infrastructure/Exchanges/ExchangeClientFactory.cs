using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Infrastructure.Exchanges;

public interface IExchangeClientFactory
{
    IExchangeClient CreateClient(string exchangeCode, string apiKey, string apiSecret, string? passphrase = null);
}

public class ExchangeClientFactory : IExchangeClientFactory
{
    public IExchangeClient CreateClient(string exchangeCode, string apiKey, string apiSecret, string? passphrase = null)
    {
        return exchangeCode.ToUpperInvariant() switch
        {
            "BINANCE" => new BinanceClient(apiKey, apiSecret),
            _ => throw new NotSupportedException($"Exchange {exchangeCode} is not supported")
        };
    }
}
