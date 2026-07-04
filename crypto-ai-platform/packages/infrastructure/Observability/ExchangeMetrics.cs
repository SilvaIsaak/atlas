using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace CryptoAIPlatform.Infrastructure.Observability;

public static class ExchangeMetrics
{
    public static string MeterName = "CryptoAIPlatform.Infrastructure.Exchanges";
    public static readonly Meter Meter = new(MeterName, "1.0.0");

    public static readonly Counter<long> RequestCount = Meter.CreateCounter<long>("exchange.request.count", description: "Total number of exchange requests made");
    public static readonly Histogram<double> RequestDuration = Meter.CreateHistogram<double>("exchange.request.duration", "ms", "Duration of exchange requests in milliseconds");
    public static readonly Counter<long> ErrorCount = Meter.CreateCounter<long>("exchange.errors.count", description: "Total number of exchange errors encountered");
    public static readonly Counter<long> ReconnectionCount = Meter.CreateCounter<long>("exchange.reconnect.count", description: "Total number of reconnection attempts made");
    public static readonly Counter<long> WebSocketDisconnectCount = Meter.CreateCounter<long>("exchange.websocket.disconnect.count", description: "Total number of WebSocket disconnects");
    public static readonly Counter<long> ThroughputCount = Meter.CreateCounter<long>("exchange.throughput.count", description: "Total number of messages processed");
}
