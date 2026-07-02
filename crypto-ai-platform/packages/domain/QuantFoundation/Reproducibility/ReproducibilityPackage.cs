using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class ReproducibilityPackage : BaseEntity<Guid>, IAggregateRoot
{
    public Guid ExperimentRunId { get; private set; }
    public EnvironmentInfo EnvironmentInfo { get; private set; } = null!;
    public ReproducibilityPackageStatus Status { get; private set; }

    private ReproducibilityPackage() { }

    public static ReproducibilityPackage Create(
        Guid id,
        TenantId tenantId,
        Guid experimentRunId,
        EnvironmentInfo environmentInfo,
        Guid? createdBy = null)
    {
        return new ReproducibilityPackage
        {
            Id = id,
            TenantId = tenantId,
            ExperimentRunId = experimentRunId,
            EnvironmentInfo = environmentInfo,
            Status = ReproducibilityPackageStatus.Pending,
            CreatedBy = createdBy
        };
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
    }

    public void Fail()
    {
        Status = ReproducibilityPackageStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }
}