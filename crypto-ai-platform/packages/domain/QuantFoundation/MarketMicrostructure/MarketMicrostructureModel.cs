using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Events;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class MarketMicrostructureModel : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<SpreadData> _spreadData = [];
    private readonly List<SlippageModel> _slippageModels = [];
    private readonly List<LiquidityProfile> _liquidityProfiles = [];
    private readonly List<OrderBookLevel> _orderBookLevels = [];
    private readonly List<BidAskSpread> _bidAskSpreads = [];
    private readonly List<LiquiditySnapshot> _liquiditySnapshots = [];
    private readonly List<MarketImpact> _marketImpacts = [];
    private readonly List<TradeFlow> _tradeFlows = [];
    private readonly List<OrderImbalance> _orderImbalances = [];
    private readonly List<VolumeProfile> _volumeProfiles = [];
    private readonly List<VWAPSnapshot> _vwapSnapshots = [];
    private readonly List<TWAPSnapshot> _twapSnapshots = [];
    private readonly List<MarketDepthSnapshot> _marketDepthSnapshots = [];

    public string Name { get; private set; } = string.Empty;
    public string AssetSymbol { get; private set; } = string.Empty;
    public DateTime CalibratedAt { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<SpreadData> SpreadData => _spreadData.AsReadOnly();
    public IReadOnlyCollection<SlippageModel> SlippageModels => _slippageModels.AsReadOnly();
    public IReadOnlyCollection<LiquidityProfile> LiquidityProfiles => _liquidityProfiles.AsReadOnly();
    public IReadOnlyCollection<OrderBookLevel> OrderBookLevels => _orderBookLevels.AsReadOnly();
    public IReadOnlyCollection<BidAskSpread> BidAskSpreads => _bidAskSpreads.AsReadOnly();
    public IReadOnlyCollection<LiquiditySnapshot> LiquiditySnapshots => _liquiditySnapshots.AsReadOnly();
    public IReadOnlyCollection<MarketImpact> MarketImpacts => _marketImpacts.AsReadOnly();
    public IReadOnlyCollection<TradeFlow> TradeFlows => _tradeFlows.AsReadOnly();
    public IReadOnlyCollection<OrderImbalance> OrderImbalances => _orderImbalances.AsReadOnly();
    public IReadOnlyCollection<VolumeProfile> VolumeProfiles => _volumeProfiles.AsReadOnly();
    public IReadOnlyCollection<VWAPSnapshot> VWAPSnapshots => _vwapSnapshots.AsReadOnly();
    public IReadOnlyCollection<TWAPSnapshot> TWAPSnapshots => _twapSnapshots.AsReadOnly();
    public IReadOnlyCollection<MarketDepthSnapshot> MarketDepthSnapshots => _marketDepthSnapshots.AsReadOnly();

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

    public void AddOrderBookLevel(OrderBookLevel level)
    {
        _orderBookLevels.Add(level);
    }

    public void AddBidAskSpread(BidAskSpread spread)
    {
        _bidAskSpreads.Add(spread);
    }

    public void AddLiquiditySnapshot(LiquiditySnapshot snapshot)
    {
        _liquiditySnapshots.Add(snapshot);
        AddDomainEvent(new LiquidityCalculatedV1(TenantId, Id, snapshot.LiquidityScore.Value, snapshot.AssetSymbol));
    }

    public void AddMarketImpact(MarketImpact impact)
    {
        _marketImpacts.Add(impact);
        AddDomainEvent(new MarketImpactCalculatedV1(TenantId, Id, impact.ImpactCost.Bps, impact.AssetSymbol));
    }

    public void AddTradeFlow(TradeFlow flow)
    {
        _tradeFlows.Add(flow);
    }

    public void AddOrderImbalance(OrderImbalance imbalance)
    {
        _orderImbalances.Add(imbalance);
    }

    public void AddVolumeProfile(VolumeProfile profile)
    {
        _volumeProfiles.Add(profile);
        AddDomainEvent(new VolumeProfileGeneratedV1(TenantId, Id, profile.AssetSymbol));
    }

    public void AddVWAPSnapshot(VWAPSnapshot snapshot)
    {
        _vwapSnapshots.Add(snapshot);
    }

    public void AddTWAPSnapshot(TWAPSnapshot snapshot)
    {
        _twapSnapshots.Add(snapshot);
    }

    public void AddMarketDepthSnapshot(MarketDepthSnapshot snapshot)
    {
        _marketDepthSnapshots.Add(snapshot);
        AddDomainEvent(new OrderBookSnapshotCreatedV1(TenantId, Id, snapshot.AssetSymbol));
    }
}