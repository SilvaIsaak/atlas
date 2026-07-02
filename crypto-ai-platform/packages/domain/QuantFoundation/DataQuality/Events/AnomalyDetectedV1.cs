using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality.Events;

public class AnomalyDetectedV1 : DomainEvent
{
    public Guid AnomalyId { get; init; }
    public string AssetSymbol { get; init; } = string.Empty;
    public string AnomalyType { get; init; } = string.Empty;
    public string Severity { get; init; } = string.Empty;

    public AnomalyDetectedV1(TenantId tenantId, Guid anomalyId, string assetSymbol, string anomalyType, string severity)
    {
        TenantId = tenantId;
        AnomalyId = anomalyId;
        AssetSymbol = assetSymbol;
        AnomalyType = anomalyType;
        Severity = severity;
    }
}