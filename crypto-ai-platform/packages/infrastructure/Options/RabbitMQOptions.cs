namespace CryptoAIPlatform.Infrastructure.Options;

public class RabbitMQOptions
{
    public const string SectionName = "RabbitMQ";
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public int RetryCount { get; set; } = 3;
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);
    public string ExchangeName { get; set; } = "cryptoaiplatform.events";
    public string DeadLetterExchangeName { get; set; } = "cryptoaiplatform.events.dlx";
    public string PoisonQueueName { get; set; } = "cryptoaiplatform.events.poison";
}
