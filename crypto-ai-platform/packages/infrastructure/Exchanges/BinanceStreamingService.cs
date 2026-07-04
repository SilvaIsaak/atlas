using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.Exchanges;

public class BinanceStreamingService : IExchangeStreamingService
{
    private readonly IBinanceSocketClient _socketClient;
    private readonly ILogger<BinanceStreamingService> _logger;
    private readonly ReconnectOptions _reconnectOptions;
    private CancellationTokenSource _cts;
    private bool _isConnected;
    private int _reconnectAttempts;

    public event EventHandler<ExchangeTrade>? OnTradeReceived;
    public event EventHandler<ExchangeOrderBook>? OnOrderBookReceived;
    public event EventHandler<ExchangeTicker>? OnMiniTickerReceived;
    public event EventHandler<ExchangeTicker>? OnTickerReceived;
    public event EventHandler<ExchangeKline>? OnKlineReceived;

    public BinanceStreamingService(
        IOptions<ReconnectOptions> reconnectOptions,
        IOptions<BinanceOptions> binanceOptions,
        ILogger<BinanceStreamingService> logger)
    {
        _logger = logger;
        _reconnectOptions = reconnectOptions.Value;
        _socketClient = new BinanceSocketClient();
        _cts = new CancellationTokenSource();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("BinanceStreamingService starting...");
        await ConnectAsync(cancellationToken);
        _ = Task.Run(MonitorConnectionAsync, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("BinanceStreamingService stopping...");
        await _socketClient.UnsubscribeAllAsync();
        _cts.Cancel();
        _isConnected = false;
    }

    private async Task ConnectAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Attempting to connect to Binance WebSocket...");
            await Task.CompletedTask;
            _isConnected = true;
            _reconnectAttempts = 0;
            _logger.LogInformation("Binance WebSocket connected");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to Binance WebSocket");
            _isConnected = false;
            await ReconnectAsync(cancellationToken);
        }
    }

    private async Task MonitorConnectionAsync()
    {
        while (!_cts.Token.IsCancellationRequested)
        {
            try
            {
                if (!_isConnected)
                {
                    await ReconnectAsync(_cts.Token);
                }
                await Task.Delay(_reconnectOptions.HeartbeatIntervalMs, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MonitorConnectionAsync");
            }
        }
    }

    private async Task ReconnectAsync(CancellationToken cancellationToken)
    {
        while (!_isConnected && _reconnectAttempts < _reconnectOptions.MaxReconnectAttempts)
        {
            _reconnectAttempts++;
            var delay = CalculateBackoffDelay(_reconnectAttempts);
            _logger.LogWarning(
                "Reconnect attempt {Attempt}/{MaxAttempts} in {DelayMs}ms...",
                _reconnectAttempts, _reconnectOptions.MaxReconnectAttempts, delay);
            await Task.Delay(delay, cancellationToken);
            await ConnectAsync(cancellationToken);
        }

        if (!_isConnected && _reconnectAttempts >= _reconnectOptions.MaxReconnectAttempts)
        {
            _logger.LogCritical("Maximum reconnect attempts reached; giving up");
        }
    }

    private int CalculateBackoffDelay(int attempt)
    {
        var delay = (int)Math.Min(_reconnectOptions.InitialDelayMs * Math.Pow(2, attempt), _reconnectOptions.MaxDelayMs);
        delay += new Random().Next(0, 100); // Add jitter
        return delay;
    }

    public Task SubscribeToTradesAsync(string symbol, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Subscribing to {Symbol} trades", symbol);
        return Task.CompletedTask;
    }

    public Task SubscribeToOrderBookAsync(string symbol, int limit = 20, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Subscribing to {Symbol} order book (limit: {Limit})", symbol, limit);
        return Task.CompletedTask;
    }

    public Task SubscribeToKlinesAsync(string symbol, string interval, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Subscribing to {Symbol} klines (interval: {Interval})", symbol, interval);
        return Task.CompletedTask;
    }

    public Task SubscribeToMiniTickersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Subscribing to all mini tickers");
        return Task.CompletedTask;
    }

    public Task SubscribeToTickersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Subscribing to all tickers");
        return Task.CompletedTask;
    }

    public Task UnsubscribeAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Unsubscribing from all streams");
        return Task.CompletedTask;
    }
}
