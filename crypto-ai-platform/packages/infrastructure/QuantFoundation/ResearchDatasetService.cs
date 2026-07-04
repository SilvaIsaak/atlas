using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.QuantFoundation;

public class ResearchDatasetService : IResearchDatasetService
{
    private readonly IResearchDatasetRepository _datasetRepository;
    private readonly IDatasetVersionRepository _versionRepository;
    private readonly ILogger<ResearchDatasetService> _logger;

    public ResearchDatasetService(
        IResearchDatasetRepository datasetRepository,
        IDatasetVersionRepository versionRepository,
        ILogger<ResearchDatasetService> logger)
    {
        _datasetRepository = datasetRepository;
        _versionRepository = versionRepository;
        _logger = logger;
    }

    public async Task<ResearchDataset> CreateDatasetAsync(
        TenantId tenantId,
        string name,
        string description,
        Guid ownerId,
        string initialVersion,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating dataset with name {Name}", name);

        var dataset = ResearchDataset.Create(
            id: Guid.NewGuid(),
            tenantId: tenantId,
            name: name,
            description: description,
            ownerId: ownerId,
            version: initialVersion,
            createdBy: ownerId);

        await _datasetRepository.AddAsync(dataset, cancellationToken);

        return dataset;
    }

    public async Task<DatasetVersion> CreateVersionAsync(
        TenantId tenantId,
        Guid datasetId,
        string version,
        DateTime periodStart,
        DateTime periodEnd,
        IReadOnlyList<string> assetSymbols,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating version {Version} for dataset {DatasetId}", version, datasetId);

        var dataset = await _datasetRepository.GetByIdAsync(datasetId, cancellationToken);
        if (dataset is null)
            throw new KeyNotFoundException($"Dataset with id {datasetId} not found!");

        var datasetVersion = DatasetVersion.Create(
            id: Guid.NewGuid(),
            tenantId: tenantId,
            datasetId: datasetId,
            version: version,
            periodStart: periodStart,
            periodEnd: periodEnd,
            assetSymbols: assetSymbols,
            createdBy: dataset.OwnerId);

        dataset.AddVersion(datasetVersion);

        await _datasetRepository.UpdateAsync(dataset, cancellationToken);

        return datasetVersion;
    }

    public async Task<DatasetVersion> PublishVersionAsync(
        TenantId tenantId,
        Guid datasetId,
        Guid versionId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing version {VersionId}", versionId);

        var version = await _versionRepository.GetByIdAsync(versionId, cancellationToken);
        if (version is null)
            throw new KeyNotFoundException($"Dataset version with id {versionId} not found!");

        version.Publish();

        await _versionRepository.UpdateAsync(version, cancellationToken);

        return version;
    }

    public async Task<DatasetVersion> ArchiveVersionAsync(
        TenantId tenantId,
        Guid datasetId,
        Guid versionId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Archiving version {VersionId}", versionId);

        var version = await _versionRepository.GetByIdAsync(versionId, cancellationToken);
        if (version is null)
            throw new KeyNotFoundException($"Dataset version with id {versionId} not found!");

        version.Archive();

        await _versionRepository.UpdateAsync(version, cancellationToken);

        return version;
    }

    public async Task<IReadOnlyList<DatasetVersion>> CompareVersionsAsync(
        TenantId tenantId,
        Guid datasetId,
        Guid versionId1,
        Guid versionId2,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Comparing versions {V1} and {V2}", versionId1, versionId2);

        var v1 = await _versionRepository.GetByIdAsync(versionId1, cancellationToken);
        var v2 = await _versionRepository.GetByIdAsync(versionId2, cancellationToken);

        if (v1 is null || v2 is null)
            throw new KeyNotFoundException("One or both dataset versions not found!");

        return new List<DatasetVersion> { v1, v2 };
    }

    public async Task<DatasetHash> CalculateHashAsync(
        TenantId tenantId,
        Guid versionId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Calculating hash for version {VersionId}", versionId);
        // Implementation of actual hash calculation would go here
        return new DatasetHash("SHA256", "dummy_hash_value_placeholder");
    }

    public async Task<(bool IsValid, string? Error)> ValidateIntegrityAsync(
        TenantId tenantId,
        Guid versionId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Validating integrity for version {VersionId}", versionId);
        // Implementation of actual integrity validation would go here
        return (true, null);
    }

    public async Task<IReadOnlyList<ResearchDataset>> GetDatasetHistoryAsync(
        TenantId tenantId,
        Guid ownerId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching dataset history for owner {OwnerId}", ownerId);
        return await _datasetRepository.GetByUserIdAsync(tenantId, ownerId, cancellationToken);
    }
}
