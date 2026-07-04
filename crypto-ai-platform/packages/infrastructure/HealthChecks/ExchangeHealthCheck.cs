using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.HealthChecks;

public class ExchangeHealthCheck : IHealthCheck
{
    private readonly IExchangeConnector _exchangeConnector;
    private readonly ILogger<ExchangeHealthCheck> _logger;
    private readonly BinanceOptions _binanceOptions;

    public ExchangeHealthCheck(
        IExchangeConnector exchangeConnector,
        IOptions<BinanceOptions> binanceOptions,
        ILogger<ExchangeHealthCheck> logger)
    {
        _exchangeConnector = exchangeConnector;
        _binanceOptions = binanceOptions.Value;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Running ExchangeHealthCheck started");

            var pingResult = await _exchangeConnector.HealthService.CheckRestHealthAsync(cancellationToken);
            var authResult = await _exchangeConnector.AuthenticationService.ValidateCredentialsAsync(cancellationToken);

            var healthData = new Dictionary<string, object>
            {
                { "Exchange", "Binance" },
                { "PingResult", pingResult },
                { "AuthResult", authResult }
            };

            if (pingResult && authResult)
            {
                _logger.LogInformation("ExchangeHealthCheck passed");
                return HealthCheckResult.Healthy("Exchange health check passed", healthData);
            }
            else
            {
                _logger.LogWarning("ExchangeHealthCheck partially failed");
                return HealthCheckResult.Degraded("Exchange health check partially failed", data: healthData);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ExchangeHealthCheck failed");
            return HealthCheckResult.Unhealthy(description: "Exchange health check failed", exception: ex);
        }
    }
}
