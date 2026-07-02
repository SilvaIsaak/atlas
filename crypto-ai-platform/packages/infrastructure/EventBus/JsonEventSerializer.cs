using System.Text.Json;
using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.Abstractions.Events;

namespace CryptoAIPlatform.Infrastructure.EventBus;

public class JsonEventSerializer : IEventSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public string Serialize<TEvent>(TEvent @event) where TEvent : DomainEvent
    {
        return JsonSerializer.Serialize(@event, Options);
    }

    public TEvent Deserialize<TEvent>(string data) where TEvent : DomainEvent
    {
        return JsonSerializer.Deserialize<TEvent>(data, Options) 
               ?? throw new InvalidOperationException("Failed to deserialize event.");
    }
}
