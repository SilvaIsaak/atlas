using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;

public interface ILiquidityRepository
{
    Task<LiquiditySnapshot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LiquiditySnapshot>> GetByModelIdAsync(Guid modelId, TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(LiquiditySnapshot snapshot, CancellationToken cancellationToken = default);
}
