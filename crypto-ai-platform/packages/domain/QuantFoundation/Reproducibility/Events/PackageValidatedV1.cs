using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;

public class PackageValidatedV1 : DomainEvent
{
    public Guid PackageId { get; init; }
    public bool IsValid { get; init; }
    public string? ValidationError { get; init; }

    public PackageValidatedV1(TenantId tenantId, Guid packageId, bool isValid, string? validationError = null)
    {
        TenantId = tenantId;
        PackageId = packageId;
        IsValid = isValid;
        ValidationError = validationError;
    }
}
