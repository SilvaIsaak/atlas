using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;

public class ReproducibilityPackageCreatedV1 : DomainEvent
{
    public Guid PackageId { get; init; }
    public Guid ExperimentRunId { get; init; }

    public ReproducibilityPackageCreatedV1(TenantId tenantId, Guid packageId, Guid experimentRunId)
    {
        TenantId = tenantId;
        PackageId = packageId;
        ExperimentRunId = experimentRunId;
    }
}