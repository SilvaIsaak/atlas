using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class ExecutionManifest : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public string ManifestJson { get; private set; } = string.Empty;
    public ManifestHash Hash { get; private set; } = null!;
    public ExecutionFingerprint Fingerprint { get; private set; } = null!;

    private ExecutionManifest() { }

    public static ExecutionManifest Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        string manifestJson,
        ManifestHash hash,
        ExecutionFingerprint fingerprint,
        Guid? createdBy = null)
    {
        return new ExecutionManifest
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            ManifestJson = manifestJson,
            Hash = hash,
            Fingerprint = fingerprint,
            CreatedBy = createdBy
        };
    }
}
