using CryptoAIPlatform.Domain.Exchanges;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoAIPlatform.Infrastructure.Exchanges;

public interface IExchangeClientFactory
{
    IExchangeConnector CreateClient(string exchangeCode, string apiKey, string apiSecret, string? passphrase = null);
}

public class ExchangeClientFactory : IExchangeClientFactory
{
    private readonly IServiceProvider _provider;

    public ExchangeClientFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IExchangeConnector CreateClient(string exchangeCode, string apiKey, string apiSecret, string? passphrase = null)
    {
        return exchangeCode.ToUpperInvariant() switch
        {
            "BINANCE" => _provider.GetService<BinanceSpotConnector>() ?? throw new NotSupportedException("Binance connector not registered"),
            _ => throw new NotSupportedException($"Exchange {exchangeCode} is not supported")
        };
    }
}
