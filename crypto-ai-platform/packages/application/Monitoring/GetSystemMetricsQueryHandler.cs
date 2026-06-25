using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Monitoring;

public class GetSystemMetricsQueryHandler : IRequestHandler<GetSystemMetricsQuery, List<GetSystemMetricResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetSystemMetricsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetSystemMetricResponse>> Handle(GetSystemMetricsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.SystemMetrics.AsQueryable();

        if (request.Type.HasValue)
        {
            query = query.Where(m => m.Type == request.Type.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(m => m.Timestamp >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(m => m.Timestamp <= request.EndDate.Value);
        }

        var metrics = await query
            .OrderByDescending(m => m.Timestamp)
            .Take(request.Limit ?? 100)
            .Select(m => new GetSystemMetricResponse(
                m.Id,
                m.Type,
                m.Value,
                m.Unit,
                m.Source,
                m.Timestamp,
                m.CreatedAt,
                m.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return metrics;
    }
}
