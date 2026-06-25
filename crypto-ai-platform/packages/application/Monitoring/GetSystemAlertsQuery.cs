using MediatR;
using CryptoAIPlatform.Domain.Monitoring;

namespace CryptoAIPlatform.Application.Monitoring;

public record GetSystemAlertsQuery : IRequest<List<GetSystemAlertResponse>>
{
    public bool? OnlyUnacknowledged { get; init; }
    public string? Severity { get; init; }
    public int? Limit { get; init; } = 50;
}

public record GetSystemAlertResponse(
    Guid AlertId,
    MetricType RelatedMetricType,
    string Title,
    string Message,
    string Severity,
    bool Acknowledged,
    DateTime? AcknowledgedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
