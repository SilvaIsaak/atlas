using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;

public class ReproducibilityVerifiedV1 : DomainEvent
{
    public Guid PackageId { get; init; }
    public bool IsVerified { get; init; }

    public ReproducibilityVerifiedV1(TenantId tenantId, Guid packageId, bool isVerified)
    {
        TenantId = tenantId;
        PackageId = packageId;
        IsVerified = isVerified;
    }
}