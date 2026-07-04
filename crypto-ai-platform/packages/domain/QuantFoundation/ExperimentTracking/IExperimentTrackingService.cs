using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

public interface IExperimentTrackingService
{
    Task<Experiment> CreateExperimentAsync(
        TenantId tenantId,
        string name,
        string description,
        ExperimentType type,
        Guid ownerId,
        Dictionary<string, string> parameters,
        CancellationToken cancellationToken = default);

    Task<ExperimentRun> StartExperimentRunAsync(
        TenantId tenantId,
        Guid experimentId,
        Guid? datasetVersionId = null,
        string? strategyVersion = null,
        Guid? startedBy = null,
        CancellationToken cancellationToken = default);

    Task<ExperimentRun> CompleteExperimentRunAsync(
        TenantId tenantId,
        Guid runId,
        Dictionary<string, (string name, decimal value)> metrics,
        CancellationToken cancellationToken = default);

    Task<ExperimentRun> FailExperimentRunAsync(
        TenantId tenantId,
        Guid runId,
        string errorMessage,
        CancellationToken cancellationToken = default);

    Task<ExperimentMetric> RecordMetricAsync(
        TenantId tenantId,
        Guid runId,
        string key,
        string name,
        decimal value,
        CancellationToken cancellationToken = default);

    Task<ExperimentArtifact> StoreArtifactAsync(
        TenantId tenantId,
        Guid runId,
        string name,
        ExperimentArtifactType type,
        string storagePath,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Experiment>> GetExperimentHistoryAsync(
        TenantId tenantId,
        Guid ownerId,
        CancellationToken cancellationToken = default);
}
