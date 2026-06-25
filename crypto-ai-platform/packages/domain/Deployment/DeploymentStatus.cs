namespace CryptoAIPlatform.Domain.Deployment;

public enum DeploymentStatus
{
    Pending,
    Building,
    Deploying,
    Successful,
    Failed,
    RolledBack
}
