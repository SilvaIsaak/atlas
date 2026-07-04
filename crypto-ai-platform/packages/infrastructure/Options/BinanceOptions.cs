namespace CryptoAIPlatform.Infrastructure.Options;

public class BinanceOptions
{
    public const string SectionName = "Binance";
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string? Passphrase { get; set; }
    public BinanceSpotOptions Spot { get; set; } = new BinanceSpotOptions();
    public BinanceFuturesOptions Futures { get; set; } = new BinanceFuturesOptions();
}

public class BinanceSpotOptions
{
    public string ApiBaseUrl { get; set; } = "https://api.binance.com";
    public string WsUrl { get; set; } = "wss://stream.binance.com:9443";
}

public class BinanceFuturesOptions
{
    public string ApiBaseUrl { get; set; } = "https://fapi.binance.com";
    public string WsUrl { get; set; } = "wss://fstream.binance.com";
}
