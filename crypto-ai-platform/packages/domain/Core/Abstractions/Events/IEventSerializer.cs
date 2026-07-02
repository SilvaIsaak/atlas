using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Core.Abstractions.Events;

public interface IEventSerializer
{
    string Serialize<TEvent>(TEvent @event) where TEvent : DomainEvent;
    TEvent Deserialize<TEvent>(string serialized) where TEvent : DomainEvent;
}
