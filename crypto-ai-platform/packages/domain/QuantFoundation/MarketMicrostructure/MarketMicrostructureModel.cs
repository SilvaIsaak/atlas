using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class MarketMicrostructureModel : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<SpreadData> _spreadData = [];
    private readonly List<SlippageModel> _slippageModels = [];
    private readonly List<LiquidityProfile> _liquidityProfiles = [];

    public string Name { get; private set; } = string.Empty;
    public string AssetSymbol { get; private set; } = string.Empty;
    public DateTime CalibratedAt { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<SpreadData> SpreadData => _spreadData.AsReadOnly();
    public IReadOnlyCollection<SlippageModel> SlippageModels => _slippageModels.AsReadOnly();
    public IReadOnlyCollection<LiquidityProfile> LiquidityProfiles => _liquidityProfiles.AsReadOnly();

    private MarketMicrostructureModel() { }

    public static MarketMicrostructureModel Create(
        Guid id,
        TenantId tenantId,
        string name,
        string assetSymbol,
        bool isActive = true,
        Guid? createdBy = null)
    {
        return new MarketMicrostructureModel
        {
            Id = id,
            TenantId = tenantId,
            Name = name,
            AssetSymbol = assetSymbol,
            IsActive = isActive,
            CalibratedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    public void AddSpreadData(SpreadData data)
    {
        _spreadData.Add(data);
    }

    public void AddSlippageModel(SlippageModel model)
    {
        _slippageModels.Add(model);
    }

    public void AddLiquidityProfile(LiquidityProfile profile)
    {
        _liquidityProfiles.Add(profile);
    }
}