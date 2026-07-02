using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality.Repositories;

public interface IDataQualityJobRepository
{
    Task<DataQualityJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DataQualityJob>> GetAllAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(DataQualityJob job, CancellationToken cancellationToken = default);
    Task UpdateAsync(DataQualityJob job, CancellationToken cancellationToken = default);
}