using MediatR;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Abstractions;

public abstract class DomainEvent : INotification
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public EventVersion Version { get; init; } = EventVersion.V1_0_0;
    public CorrelationId CorrelationId { get; init; } = CorrelationId.New();
    public CausationId? CausationId { get; init; }
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    public TenantId TenantId { get; init; } = null!;
    public IdempotencyKey IdempotencyKey { get; init; } = IdempotencyKey.New();
}
