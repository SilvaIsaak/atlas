using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Abstractions;

public abstract class BaseEntity<TId> where TId : IEquatable<TId>
{
    private readonly List<DomainEvent> _domainEvents = [];

    protected BaseEntity()
    {
    }

    protected BaseEntity(TId id)
    {
        Id = id;
    }

    public TId Id { get; init; } = default!;
    public TenantId TenantId { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right) => Equals(left, right);
    public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right) => !Equals(left, right);
}
