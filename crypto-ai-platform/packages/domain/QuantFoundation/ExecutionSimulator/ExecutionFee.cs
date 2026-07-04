using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class ExecutionFee : BaseEntity<Guid>
{
    public Guid SimulationId { get; private set; }
    public Guid? OrderId { get; private set; }
    public Guid? FillId { get; private set; }
    public FeeAmount Fee { get; private set; } = null!;

    private ExecutionFee() { }

    public static ExecutionFee Create(
        Guid id,
        TenantId tenantId,
        Guid simulationId,
        Guid? orderId,
        Guid? fillId,
        FeeAmount fee,
        Guid? createdBy = null)
    {
        return new ExecutionFee
        {
            Id = id,
            TenantId = tenantId,
            SimulationId = simulationId,
            OrderId = orderId,
            FillId = fillId,
            Fee = fee,
            CreatedBy = createdBy
        };
    }
}
