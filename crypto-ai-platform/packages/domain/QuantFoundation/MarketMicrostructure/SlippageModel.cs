using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class SlippageModel : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;
    public string? Parameters { get; private set; }

    private SlippageModel() { }

    public static SlippageModel Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        string assetSymbol,
        string? parameters = null,
        Guid? createdBy = null)
    {
        return new SlippageModel
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            AssetSymbol = assetSymbol,
            Parameters = parameters,
            CreatedBy = createdBy
        };
    }
}