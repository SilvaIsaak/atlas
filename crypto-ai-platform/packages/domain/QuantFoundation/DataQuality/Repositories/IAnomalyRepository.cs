using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality.Repositories;

public interface IAnomalyRepository
{
    Task<Anomaly?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Anomaly>> GetByAssetSymbolAndPeriodAsync(string assetSymbol, DateTime periodStart, DateTime periodEnd, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Anomaly>> GetUnresolvedAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(Anomaly anomaly, CancellationToken cancellationToken = default);
    Task UpdateAsync(Anomaly anomaly, CancellationToken cancellationToken = default);
}