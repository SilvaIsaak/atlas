using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Workers;

public class MarketDataWorker : BackgroundService
{
    private readonly IExchangeConnector _exchangeConnector;
    private readonly IExchangeStreamingService _streamingService;
    private readonly ILogger<MarketDataWorker> _logger;

    public MarketDataWorker(
        IExchangeConnector exchangeConnector,
        IExchangeStreamingService streamingService,
        ILogger<MarketDataWorker> logger)
    {
        _exchangeConnector = exchangeConnector;
        _streamingService = streamingService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MarketDataWorker starting...");
        await _streamingService.StartAsync(stoppingToken);
        await _streamingService.SubscribeToMiniTickersAsync(stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
        await _streamingService.StopAsync(stoppingToken);
    }
}
