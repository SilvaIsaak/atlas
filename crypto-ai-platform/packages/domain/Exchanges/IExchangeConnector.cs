namespace CryptoAIPlatform.Domain.Exchanges;

public interface IExchangeConnector
{
    IExchangeMarketDataService MarketDataService { get; }
    IExchangeTradingService TradingService { get; }
    IExchangeAuthenticationService AuthenticationService { get; }
    IExchangeHealthService HealthService { get; }
}
