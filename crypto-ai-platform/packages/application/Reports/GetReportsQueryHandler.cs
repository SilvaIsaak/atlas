using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Reports;

public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, List<GetReportResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetReportsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetReportResponse>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Reports.Where(r => r.UserId == request.UserId);

        if (request.Type.HasValue)
        {
            query = query.Where(r => r.Type == request.Type.Value);
        }

        var reports = await query
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new GetReportResponse(
                r.Id,
                r.UserId,
                r.Type,
                r.Name,
                r.Description,
                r.StartDate,
                r.EndDate,
                r.FilePath,
                r.FileUrl,
                r.IsGenerated,
                r.GeneratedAt,
                r.CreatedAt,
                r.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return reports;
    }
}
