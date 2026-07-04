using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoAIPlatform.Infrastructure.Workers;

public class DataQualityWorker : BackgroundService
{
    private readonly ILogger<DataQualityWorker> _logger;

    public DataQualityWorker(ILogger<DataQualityWorker> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DataQualityWorker starting...");
        return Task.CompletedTask;
    }
}
