using System;
using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Reports;

public class Report : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public ReportType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? FilePath { get; set; }
    public string? FileUrl { get; set; }
    public bool IsGenerated { get; set; }
    public DateTime? GeneratedAt { get; set; }
}
