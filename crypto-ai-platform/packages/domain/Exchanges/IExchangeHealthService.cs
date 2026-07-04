namespace CryptoAIPlatform.Domain.Exchanges;

public interface IExchangeHealthService
{
    Task<bool> CheckRestHealthAsync(CancellationToken cancellationToken = default);
    Task<bool> CheckWebSocketHealthAsync(CancellationToken cancellationToken = default);
    Task<TimeSpan> GetLatencyAsync(CancellationToken cancellationToken = default);
}
