using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Monitoring;

public class GetSystemAlertsQueryHandler : IRequestHandler<GetSystemAlertsQuery, List<GetSystemAlertResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetSystemAlertsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetSystemAlertResponse>> Handle(GetSystemAlertsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.SystemAlerts.AsQueryable();

        if (request.OnlyUnacknowledged.HasValue)
        {
            query = query.Where(a => a.Acknowledged == !request.OnlyUnacknowledged.Value);
        }

        if (!string.IsNullOrEmpty(request.Severity))
        {
            query = query.Where(a => a.Severity == request.Severity);
        }

        var alerts = await query
            .OrderByDescending(a => a.CreatedAt)
            .Take(request.Limit ?? 50)
            .Select(a => new GetSystemAlertResponse(
                a.Id,
                a.RelatedMetricType,
                a.Title,
                a.Message,
                a.Severity,
                a.Acknowledged,
                a.AcknowledgedAt,
                a.CreatedAt,
                a.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return alerts;
    }
}
