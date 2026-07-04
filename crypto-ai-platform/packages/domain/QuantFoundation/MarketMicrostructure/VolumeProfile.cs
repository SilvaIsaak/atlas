using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class VolumeProfile : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public List<VolumeBucket> VolumeBuckets { get; private set; } = new();
    public DateTime StartTimestamp { get; private set; }
    public DateTime EndTimestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private VolumeProfile() { }

    public static VolumeProfile Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        List<VolumeBucket> volumeBuckets,
        DateTime startTimestamp,
        DateTime endTimestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new VolumeProfile
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            VolumeBuckets = volumeBuckets,
            StartTimestamp = startTimestamp,
            EndTimestamp = endTimestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
