using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public class FundingRate : BaseEntity<Guid>
{
    public string Exchange { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
    public DateTime FundingTimeUtc { get; set; }
    public decimal Rate { get; set; }
}
