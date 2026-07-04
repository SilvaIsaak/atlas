using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public interface IResearchDatasetService
{
    Task<ResearchDataset> CreateDatasetAsync(
        TenantId tenantId,
        string name,
        string description,
        Guid ownerId,
        string initialVersion,
        CancellationToken cancellationToken = default);

    Task<DatasetVersion> CreateVersionAsync(
        TenantId tenantId,
        Guid datasetId,
        string version,
        DateTime periodStart,
        DateTime periodEnd,
        IReadOnlyList<string> assetSymbols,
        CancellationToken cancellationToken = default);

    Task<DatasetVersion> PublishVersionAsync(
        TenantId tenantId,
        Guid datasetId,
        Guid versionId,
        CancellationToken cancellationToken = default);

    Task<DatasetVersion> ArchiveVersionAsync(
        TenantId tenantId,
        Guid datasetId,
        Guid versionId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DatasetVersion>> CompareVersionsAsync(
        TenantId tenantId,
        Guid datasetId,
        Guid versionId1,
        Guid versionId2,
        CancellationToken cancellationToken = default);

    Task<DatasetHash> CalculateHashAsync(
        TenantId tenantId,
        Guid versionId,
        CancellationToken cancellationToken = default);

    Task<(bool IsValid, string? Error)> ValidateIntegrityAsync(
        TenantId tenantId,
        Guid versionId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ResearchDataset>> GetDatasetHistoryAsync(
        TenantId tenantId,
        Guid ownerId,
        CancellationToken cancellationToken = default);
}
