using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Events;

public class ExperimentStartedV1 : DomainEvent
{
    public Guid ExperimentId { get; init; }
    public Guid RunId { get; init; }
    public Guid StartedBy { get; init; }

    public ExperimentStartedV1(TenantId tenantId, Guid experimentId, Guid runId, Guid startedBy)
    {
        TenantId = tenantId;
        ExperimentId = experimentId;
        RunId = runId;
        StartedBy = startedBy;
    }
}