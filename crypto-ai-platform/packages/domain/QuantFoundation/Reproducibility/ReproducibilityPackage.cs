using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class ReproducibilityPackage : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<EnvironmentSnapshot> _environmentSnapshots = new();
    private readonly List<GitSnapshot> _gitSnapshots = new();
    private readonly List<DependencySnapshot> _dependencySnapshots = new();
    private readonly List<DatasetReference> _datasetReferences = new();
    private readonly List<FeatureReference> _featureReferences = new();
    private readonly List<ExperimentReference> _experimentReferences = new();
    private readonly List<ArtifactReference> _artifactReferences = new();
    private readonly List<ExecutionManifest> _executionManifests = new();

    public Guid ExperimentRunId { get; private set; }
    public EnvironmentInfo EnvironmentInfo { get; private set; } = null!;
    public ReproducibilityPackageStatus Status { get; private set; }
    public PackageChecksum? Checksum { get; private set; }

    public IReadOnlyList<EnvironmentSnapshot> EnvironmentSnapshots => _environmentSnapshots.AsReadOnly();
    public IReadOnlyList<GitSnapshot> GitSnapshots => _gitSnapshots.AsReadOnly();
    public IReadOnlyList<DependencySnapshot> DependencySnapshots => _dependencySnapshots.AsReadOnly();
    public IReadOnlyList<DatasetReference> DatasetReferences => _datasetReferences.AsReadOnly();
    public IReadOnlyList<FeatureReference> FeatureReferences => _featureReferences.AsReadOnly();
    public IReadOnlyList<ExperimentReference> ExperimentReferences => _experimentReferences.AsReadOnly();
    public IReadOnlyList<ArtifactReference> ArtifactReferences => _artifactReferences.AsReadOnly();
    public IReadOnlyList<ExecutionManifest> ExecutionManifests => _executionManifests.AsReadOnly();

    private ReproducibilityPackage() { }

    public static ReproducibilityPackage Create(
        Guid id,
        TenantId tenantId,
        Guid experimentRunId,
        EnvironmentInfo environmentInfo,
        Guid? createdBy = null)
    {
        var package = new ReproducibilityPackage
        {
            Id = id,
            TenantId = tenantId,
            ExperimentRunId = experimentRunId,
            EnvironmentInfo = environmentInfo,
            Status = ReproducibilityPackageStatus.Pending,
            CreatedBy = createdBy
        };

        package.AddDomainEvent(new ReproducibilityPackageCreatedV1(tenantId, id, experimentRunId));

        return package;
    }

    public void AddEnvironmentSnapshot(EnvironmentSnapshot snapshot) => _environmentSnapshots.Add(snapshot);
    public void AddGitSnapshot(GitSnapshot snapshot) => _gitSnapshots.Add(snapshot);
    public void AddDependencySnapshot(DependencySnapshot snapshot) => _dependencySnapshots.Add(snapshot);
    public void AddDatasetReference(DatasetReference reference) => _datasetReferences.Add(reference);
    public void AddFeatureReference(FeatureReference reference) => _featureReferences.Add(reference);
    public void AddExperimentReference(ExperimentReference reference) => _experimentReferences.Add(reference);
    public void AddArtifactReference(ArtifactReference reference) => _artifactReferences.Add(reference);
    public void AddExecutionManifest(ExecutionManifest manifest) => _executionManifests.Add(manifest);

    public void SetChecksum(PackageChecksum checksum)
    {
        Checksum = checksum;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new ChecksumCalculatedV1(TenantId, Id, checksum.Algorithm, checksum.Value));
    }

    public void Start()
    {
        Status = ReproducibilityPackageStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Verify()
    {
        Status = ReproducibilityPackageStatus.Verified;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new PackageValidatedV1(TenantId, Id, true));
    }

    public void Fail(string error)
    {
        Status = ReproducibilityPackageStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new PackageValidatedV1(TenantId, Id, false, error));
    }
}