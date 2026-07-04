using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoAIPlatform.Infrastructure.Workers;

public class ExperimentWorker : BackgroundService
{
    private readonly ILogger<ExperimentWorker> _logger;

    public ExperimentWorker(ILogger<ExperimentWorker> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ExperimentWorker starting...");
        return Task.CompletedTask;
    }
}
