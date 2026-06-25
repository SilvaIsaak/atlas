using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.MultiTenant;

public class Tenant : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public bool IsActive { get; set; }
}
