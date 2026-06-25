using MediatR;
using CryptoAIPlatform.Domain.Reports;

namespace CryptoAIPlatform.Application.Reports;

public record CreateReportCommand : IRequest<CreateReportResponse>
{
    public Guid UserId { get; init; }
    public ReportType Type { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public record CreateReportResponse
{
    public Guid ReportId { get; init; }
    public bool IsGenerated { get; init; }
}
