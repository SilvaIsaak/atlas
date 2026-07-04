using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class GitSnapshot : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public GitCommitHash CommitHash { get; private set; } = null!;
    public string? Branch { get; private set; }
    public string? Tag { get; private set; }
    public string? RepositoryUrl { get; private set; }

    private GitSnapshot() { }

    public static GitSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        GitCommitHash commitHash,
        string? branch,
        string? tag,
        string? repositoryUrl,
        Guid? createdBy = null)
    {
        return new GitSnapshot
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            CommitHash = commitHash,
            Branch = branch,
            Tag = tag,
            RepositoryUrl = repositoryUrl,
            CreatedBy = createdBy
        };
    }
}
