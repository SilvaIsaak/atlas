using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public class OpenInterest : BaseEntity<Guid>
{
    public string Exchange { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
    public decimal Interest { get; set; }
}
