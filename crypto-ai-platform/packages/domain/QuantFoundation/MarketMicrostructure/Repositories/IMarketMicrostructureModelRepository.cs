using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;

public interface IMarketMicrostructureModelRepository
{
    Task<MarketMicrostructureModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MarketMicrostructureModel>> GetAllAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MarketMicrostructureModel>> GetActiveAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(MarketMicrostructureModel model, CancellationToken cancellationToken = default);
    Task UpdateAsync(MarketMicrostructureModel model, CancellationToken cancellationToken = default);
}