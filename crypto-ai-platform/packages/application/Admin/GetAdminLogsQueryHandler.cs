using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Admin;

public class GetAdminLogsQueryHandler : IRequestHandler<GetAdminLogsQuery, List<GetAdminLogResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAdminLogsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetAdminLogResponse>> Handle(GetAdminLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = await _dbContext.AdminLogs
            .OrderByDescending(l => l.CreatedAt)
            .Take(request.Limit)
            .Select(l => new GetAdminLogResponse(
                l.Id,
                l.AdminUserId,
                l.Action,
                l.TargetEntity,
                l.TargetId,
                l.Details,
                l.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return logs;
    }
}
