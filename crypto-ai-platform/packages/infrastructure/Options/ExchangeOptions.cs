namespace CryptoAIPlatform.Infrastructure.Options;

public class ExchangeOptions
{
    public const string SectionName = "Exchange";
    public string DefaultExchange { get; set; } = "BINANCE";
    public bool UseTestnet { get; set; } = false;
}
