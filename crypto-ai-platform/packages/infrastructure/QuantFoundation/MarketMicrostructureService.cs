using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.QuantFoundation;

public interface IMarketMicrostructureService
{
    Task<MarketMicrostructureModel> CreateModelAsync(TenantId tenantId, string name, string assetSymbol, CancellationToken cancellationToken = default);
    Task<Spread> CalculateSpreadAsync(TenantId tenantId, Guid modelId, decimal bidPrice, decimal askPrice, CancellationToken cancellationToken = default);
    Task<decimal> CalculateVWAPAsync(TenantId tenantId, Guid modelId, DateTime start, DateTime end, CancellationToken cancellationToken = default);
}

public class MarketMicrostructureService : IMarketMicrostructureService
{
    private readonly IMarketMicrostructureModelRepository _modelRepository;
    private readonly ILogger<MarketMicrostructureService> _logger;

    public MarketMicrostructureService(
        IMarketMicrostructureModelRepository modelRepository,
        ILogger<MarketMicrostructureService> logger)
    {
        _modelRepository = modelRepository;
        _logger = logger;
    }

    public async Task<MarketMicrostructureModel> CreateModelAsync(TenantId tenantId, string name, string assetSymbol, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating Market Microstructure model for asset: {AssetSymbol}", assetSymbol);
        var model = MarketMicrostructureModel.Create(Guid.NewGuid(), tenantId, name, assetSymbol);
        await _modelRepository.AddAsync(model, cancellationToken);
        return model;
    }

    public async Task<Spread> CalculateSpreadAsync(TenantId tenantId, Guid modelId, decimal bidPrice, decimal askPrice, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Calculating spread for model id: {ModelId}", modelId);
        var spreadBps = (askPrice - bidPrice) / bidPrice * 10000;
        return new Spread(bidPrice, askPrice, spreadBps);
    }

    public async Task<decimal> CalculateVWAPAsync(TenantId tenantId, Guid modelId, DateTime start, DateTime end, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Calculating VWAP for model: {ModelId}", modelId);
        return 0.0m; // placeholder
    }
}
