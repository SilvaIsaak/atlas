using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Events;

public class ExperimentCompletedV1 : DomainEvent
{
    public Guid ExperimentId { get; init; }
    public Guid RunId { get; init; }
    public IReadOnlyDictionary<string, decimal> Metrics { get; init; } = new Dictionary<string, decimal>();

    public ExperimentCompletedV1(TenantId tenantId, Guid experimentId, Guid runId, IReadOnlyDictionary<string, decimal> metrics)
    {
        TenantId = tenantId;
        ExperimentId = experimentId;
        RunId = runId;
        Metrics = metrics;
    }
}