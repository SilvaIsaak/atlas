using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Admin;

public class AdminLog : BaseEntity<Guid>, IAggregateRoot
{
    public Guid AdminUserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? TargetEntity { get; set; }
    public string? TargetId { get; set; }
    public string? Details { get; set; }
}
