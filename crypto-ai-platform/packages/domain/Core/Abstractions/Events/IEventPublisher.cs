using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Core.Abstractions.Events;

public interface IEventPublisher
{
    Task PublishOutboxAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : DomainEvent;
}
