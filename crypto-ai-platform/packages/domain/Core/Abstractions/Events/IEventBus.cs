using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Core.Abstractions.Events;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : DomainEvent;
    Task SubscribeAsync<TEvent, THandler>(CancellationToken cancellationToken = default) 
        where TEvent : DomainEvent 
        where THandler : IEventHandler<TEvent>;
}
