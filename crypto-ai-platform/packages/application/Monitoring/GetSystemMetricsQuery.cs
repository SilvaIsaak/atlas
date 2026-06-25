using MediatR;
using CryptoAIPlatform.Domain.Monitoring;

namespace CryptoAIPlatform.Application.Monitoring;

public record GetSystemMetricsQuery : IRequest<List<GetSystemMetricResponse>>
{
    public MetricType? Type { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int? Limit { get; init; } = 100;
}

public record GetSystemMetricResponse(
    Guid MetricId,
    MetricType Type,
    double Value,
    string Unit,
    string? Source,
    DateTime Timestamp,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
