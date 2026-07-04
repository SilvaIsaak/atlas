using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;

public class ManifestGeneratedV1 : DomainEvent
{
    public Guid PackageId { get; init; }
    public Guid ManifestId { get; init; }

    public ManifestGeneratedV1(TenantId tenantId, Guid packageId, Guid manifestId)
    {
        TenantId = tenantId;
        PackageId = packageId;
        ManifestId = manifestId;
    }
}
