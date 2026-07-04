using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public class OrderBook : BaseEntity<Guid>
{
    public string Exchange { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
    public List<OrderBookLevel> Bids { get; set; } = new();
    public List<OrderBookLevel> Asks { get; set; } = new();
}
