using MediatR;
using CryptoAIPlatform.Domain.Deployment;

namespace CryptoAIPlatform.Application.Deployment;

public record GetDeploymentsQuery : IRequest<List<GetDeploymentResponse>>
{
    public DeploymentStatus? Status { get; init; }
    public string? Environment { get; init; }
    public int? Limit { get; init; } = 50;
}

public record GetDeploymentResponse(
    Guid DeploymentId,
    string Version,
    string Description,
    string? BuildNumber,
    string? Environment,
    DeploymentStatus Status,
    string? Logs,
    DateTime? StartedAt,
    DateTime? CompletedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
