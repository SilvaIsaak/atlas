using MediatR;
using CryptoAIPlatform.Domain.Deployment;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Deployment;

public class CreateDeploymentCommandHandler : IRequestHandler<CreateDeploymentCommand, CreateDeploymentResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateDeploymentCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateDeploymentResponse> Handle(CreateDeploymentCommand request, CancellationToken cancellationToken)
    {
        var deployment = new Deployment
        {
            Id = Guid.NewGuid(),
            Version = request.Version,
            Description = request.Description,
            BuildNumber = request.BuildNumber,
            Environment = request.Environment,
            Status = DeploymentStatus.Pending
        };

        await _dbContext.Deployments.AddAsync(deployment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateDeploymentResponse
        {
            DeploymentId = deployment.Id,
            Version = deployment.Version,
            Status = deployment.Status
        };
    }
}
