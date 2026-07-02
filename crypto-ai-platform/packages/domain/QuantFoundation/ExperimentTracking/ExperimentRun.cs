using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

public class ExperimentRun : BaseEntity<Guid>
{
    private readonly List<ExperimentMetric> _metrics = [];
    private readonly List<ExperimentArtifact> _artifacts = [];

    public Guid ExperimentId { get; private set; }
    public Guid? DatasetVersionId { get; private set; }
    public string? StrategyVersion { get; private set; }
    public ExperimentRunStatus Status { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public IReadOnlyCollection<ExperimentMetric> Metrics => _metrics.AsReadOnly();
    public IReadOnlyCollection<ExperimentArtifact> Artifacts => _artifacts.AsReadOnly();

    private ExperimentRun() { }

    public static ExperimentRun Create(
        Guid id,
        TenantId tenantId,
        Guid experimentId,
        Guid? datasetVersionId = null,
        string? strategyVersion = null,
        Guid? createdBy = null)
    {
        return new ExperimentRun
        {
            Id = id,
            TenantId = tenantId,
            ExperimentId = experimentId,
            DatasetVersionId = datasetVersionId,
            StrategyVersion = strategyVersion,
            Status = ExperimentRunStatus.Draft,
            CreatedBy = createdBy
        };
    }

    public void Start()
    {
        Status = ExperimentRunStatus.Running;
        StartedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = ExperimentRunStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        Status = ExperimentRunStatus.Failed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddMetric(ExperimentMetric metric)
    {
        _metrics.Add(metric);
    }

    public void AddArtifact(ExperimentArtifact artifact)
    {
        _artifacts.Add(artifact);
    }
}