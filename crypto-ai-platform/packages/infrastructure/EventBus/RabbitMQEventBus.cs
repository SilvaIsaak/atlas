using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.Abstractions.Events;

namespace CryptoAIPlatform.Infrastructure.EventBus;

public class RabbitMQEventBus : IEventBus, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IEventSerializer _serializer;
    private readonly ILogger<RabbitMQEventBus> _logger;
    private readonly string _exchangeName = "cryptoaiplatform_events";

    public RabbitMQEventBus(IConnection connection, IEventSerializer serializer, ILogger<RabbitMQEventBus> logger)
    {
        _connection = connection;
        _serializer = serializer;
        _logger = logger;
        
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Topic, durable: true);
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : DomainEvent
    {
        var eventName = typeof(TEvent).Name;
        var message = _serializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: eventName,
            basicProperties: null,
            body: body);

        _logger.LogInformation("Published event {EventName} with id {EventId}", eventName, @event.EventId);
        
        return Task.CompletedTask;
    }

    public Task SubscribeAsync<TEvent, THandler>(CancellationToken cancellationToken = default) 
        where TEvent : DomainEvent 
        where THandler : IEventHandler<TEvent>
    {
        var eventName = typeof(TEvent).Name;
        var queueName = $"{typeof(THandler).Name}_{eventName}";
        
        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: queueName, exchange: _exchangeName, routingKey: eventName);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = _serializer.Deserialize<TEvent>(message);
            
            _logger.LogInformation("Received event {EventName} with id {EventId}", eventName, @event.EventId);

            try
            {
                // TODO: Implement proper handler activation via DI
                // For now, log and acknowledge
                _logger.LogInformation("Handling event {EventName}", eventName);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling event {EventName}", eventName);
                _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        _logger.LogInformation("Subscribed handler {HandlerName} to event {EventName}", typeof(THandler).Name, eventName);
        
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
        // Don't dispose the connection—it's a singleton shared across the app
        GC.SuppressFinalize(this);
    }
}
