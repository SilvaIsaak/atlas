using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Events;

public class ExperimentFailedV1 : DomainEvent
{
    public Guid ExperimentId { get; init; }
    public Guid RunId { get; init; }
    public string ErrorMessage { get; init; } = string.Empty;

    public ExperimentFailedV1(TenantId tenantId, Guid experimentId, Guid runId, string errorMessage)
    {
        TenantId = tenantId;
        ExperimentId = experimentId;
        RunId = runId;
        ErrorMessage = errorMessage;
    }
}