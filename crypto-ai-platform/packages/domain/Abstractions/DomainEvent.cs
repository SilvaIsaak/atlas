using MediatR;

namespace CryptoAIPlatform.Domain.Abstractions;

public abstract class DomainEvent : INotification
{
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
