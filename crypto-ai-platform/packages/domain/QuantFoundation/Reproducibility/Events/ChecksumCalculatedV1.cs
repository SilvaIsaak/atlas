using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;

public class ChecksumCalculatedV1 : DomainEvent
{
    public Guid PackageId { get; init; }
    public string Algorithm { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;

    public ChecksumCalculatedV1(TenantId tenantId, Guid packageId, string algorithm, string value)
    {
        TenantId = tenantId;
        PackageId = packageId;
        Algorithm = algorithm;
        Value = value;
    }
}
