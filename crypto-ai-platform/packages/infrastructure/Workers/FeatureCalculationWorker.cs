using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoAIPlatform.Infrastructure.Workers;

public class FeatureCalculationWorker : BackgroundService
{
    private readonly ILogger<FeatureCalculationWorker> _logger;

    public FeatureCalculationWorker(ILogger<FeatureCalculationWorker> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FeatureCalculationWorker starting...");
        return Task.CompletedTask;
    }
}
