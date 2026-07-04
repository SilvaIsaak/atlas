using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class EnvironmentSnapshot : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public DockerImageReference? DockerImage { get; private set; }
    public PythonEnvironment? PythonEnvironment { get; private set; }
    public DotNetEnvironment? DotNetEnvironment { get; private set; }
    public Dictionary<string, string> EnvironmentVariables { get; private set; } = new();

    private EnvironmentSnapshot() { }

    public static EnvironmentSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        DockerImageReference? dockerImage,
        PythonEnvironment? pythonEnvironment,
        DotNetEnvironment? dotNetEnvironment,
        Dictionary<string, string> environmentVariables,
        Guid? createdBy = null)
    {
        return new EnvironmentSnapshot
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            DockerImage = dockerImage,
            PythonEnvironment = pythonEnvironment,
            DotNetEnvironment = dotNetEnvironment,
            EnvironmentVariables = environmentVariables,
            CreatedBy = createdBy
        };
    }
}
