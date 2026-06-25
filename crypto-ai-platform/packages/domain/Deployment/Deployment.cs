using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Deployment;

public class Deployment : BaseEntity<Guid>, IAggregateRoot
{
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? BuildNumber { get; set; }
    public string? Environment { get; set; }
    public DeploymentStatus Status { get; set; } = DeploymentStatus.Pending;
    public string? Logs { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
