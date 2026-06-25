using MediatR;
using CryptoAIPlatform.Domain.Reports;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Reports;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateReportCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateReportResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var report = new Report
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Type = request.Type,
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsGenerated = false
        };

        await _dbContext.Reports.AddAsync(report, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateReportResponse
        {
            ReportId = report.Id,
            IsGenerated = report.IsGenerated
        };
    }
}
