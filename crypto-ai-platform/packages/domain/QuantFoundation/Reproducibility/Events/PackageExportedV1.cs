using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;

public class PackageExportedV1 : DomainEvent
{
    public Guid PackageId { get; init; }
    public string ExportPath { get; init; } = string.Empty;

    public PackageExportedV1(TenantId tenantId, Guid packageId, string exportPath)
    {
        TenantId = tenantId;
        PackageId = packageId;
        ExportPath = exportPath;
    }
}
