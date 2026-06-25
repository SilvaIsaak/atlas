using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Deployment;

public class GetDeploymentsQueryHandler : IRequestHandler<GetDeploymentsQuery, List<GetDeploymentResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetDeploymentsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetDeploymentResponse>> Handle(GetDeploymentsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Deployments.AsQueryable();

        if (request.Status.HasValue)
        {
            query = query.Where(d => d.Status == request.Status.Value);
        }

        if (!string.IsNullOrEmpty(request.Environment))
        {
            query = query.Where(d => d.Environment == request.Environment);
        }

        var deployments = await query
            .OrderByDescending(d => d.CreatedAt)
            .Take(request.Limit ?? 50)
            .Select(d => new GetDeploymentResponse(
                d.Id,
                d.Version,
                d.Description,
                d.BuildNumber,
                d.Environment,
                d.Status,
                d.Logs,
                d.StartedAt,
                d.CompletedAt,
                d.CreatedAt,
                d.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return deployments;
    }
}
