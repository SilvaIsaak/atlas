using MediatR;
using CryptoAIPlatform.Domain.Deployment;

namespace CryptoAIPlatform.Application.Deployment;

public record CreateDeploymentCommand : IRequest<CreateDeploymentResponse>
{
    public string Version { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? BuildNumber { get; init; }
    public string? Environment { get; init; }
}

public record CreateDeploymentResponse
{
    public Guid DeploymentId { get; init; }
    public string Version { get; init; } = string.Empty;
    public DeploymentStatus Status { get; init; }
}
