using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class ExecutionStatistics : BaseEntity<Guid>
{
    public Guid SimulationId { get; private set; }
    public int TotalOrders { get; private set; }
    public int TotalFills { get; private set; }
    public decimal TotalQuantity { get; private set; }
    public decimal AverageSlippageBps { get; private set; }
    public decimal TotalFees { get; private set; }

    private ExecutionStatistics() { }

    public static ExecutionStatistics Create(
        Guid id,
        TenantId tenantId,
        Guid simulationId,
        int totalOrders,
        int totalFills,
        decimal totalQuantity,
        decimal averageSlippageBps,
        decimal totalFees,
        Guid? createdBy = null)
    {
        return new ExecutionStatistics
        {
            Id = id,
            TenantId = tenantId,
            SimulationId = simulationId,
            TotalOrders = totalOrders,
            TotalFills = totalFills,
            TotalQuantity = totalQuantity,
            AverageSlippageBps = averageSlippageBps,
            TotalFees = totalFees,
            CreatedBy = createdBy
        };
    }
}
