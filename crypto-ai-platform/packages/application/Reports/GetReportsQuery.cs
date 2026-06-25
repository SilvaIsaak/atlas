using MediatR;
using CryptoAIPlatform.Domain.Reports;

namespace CryptoAIPlatform.Application.Reports;

public record GetReportsQuery : IRequest<List<GetReportResponse>>
{
    public Guid UserId { get; init; }
    public ReportType? Type { get; init; }
}

public record GetReportResponse(
    Guid ReportId,
    Guid UserId,
    ReportType Type,
    string Name,
    string? Description,
    DateTime StartDate,
    DateTime EndDate,
    string? FilePath,
    string? FileUrl,
    bool IsGenerated,
    DateTime? GeneratedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
