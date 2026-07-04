using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class ExecutionSlippage : BaseEntity<Guid>
{
    public Guid SimulationId { get; private set; }
    public Guid? OrderId { get; private set; }
    public Guid? FillId { get; private set; }
    public SlippageAmount Slippage { get; private set; } = null!;

    private ExecutionSlippage() { }

    public static ExecutionSlippage Create(
        Guid id,
        TenantId tenantId,
        Guid simulationId,
        Guid? orderId,
        Guid? fillId,
        SlippageAmount slippage,
        Guid? createdBy = null)
    {
        return new ExecutionSlippage
        {
            Id = id,
            TenantId = tenantId,
            SimulationId = simulationId,
            OrderId = orderId,
            FillId = fillId,
            Slippage = slippage,
            CreatedBy = createdBy
        };
    }
}
