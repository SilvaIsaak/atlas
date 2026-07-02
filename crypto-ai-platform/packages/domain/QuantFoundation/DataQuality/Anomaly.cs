using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality;

public class Anomaly : BaseEntity<Guid>
{
    public Guid JobId { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;
    public AnomalyType Type { get; private set; }
    public AnomalySeverity Severity { get; private set; }
    public bool IsResolved { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public Guid? ResolvedBy { get; private set; }

    private Anomaly() { }

    public static Anomaly Create(
        Guid id,
        TenantId tenantId,
        Guid jobId,
        string assetSymbol,
        AnomalyType type,
        AnomalySeverity severity,
        Guid? createdBy = null)
    {
        return new Anomaly
        {
            Id = id,
            TenantId = tenantId,
            JobId = jobId,
            AssetSymbol = assetSymbol,
            Type = type,
            Severity = severity,
            IsResolved = false,
            CreatedBy = createdBy
        };
    }

    public void Resolve(Guid resolvedBy)
    {
        IsResolved = true;
        ResolvedAt = DateTime.UtcNow;
        ResolvedBy = resolvedBy;
        UpdatedAt = DateTime.UtcNow;
    }
}