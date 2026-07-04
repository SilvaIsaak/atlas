using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public class Trade : BaseEntity<Guid>
{
    public string Exchange { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
    public long ExchangeTradeId { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public bool IsBuyerMaker { get; set; }
}
