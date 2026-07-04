using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class DependencySnapshot : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public Dictionary<string, string> Dependencies { get; private set; } = new();
    public DependencyChecksum Checksum { get; private set; } = null!;

    private DependencySnapshot() { }

    public static DependencySnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        Dictionary<string, string> dependencies,
        DependencyChecksum checksum,
        Guid? createdBy = null)
    {
        return new DependencySnapshot
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            Dependencies = dependencies,
            Checksum = checksum,
            CreatedBy = createdBy
        };
    }
}
