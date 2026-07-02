using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Core.Abstractions.Events;

public interface IEventHandler<TEvent> where TEvent : DomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
