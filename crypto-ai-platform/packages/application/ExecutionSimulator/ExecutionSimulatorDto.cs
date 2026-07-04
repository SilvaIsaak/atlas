namespace CryptoAIPlatform.Application.ExecutionSimulator;

public record ExecutionSimulationDto(
    Guid Id,
    string Name,
    Guid? MicrostructureModelId,
    string Status,
    DateTime CreatedAt);

public record ExecutionStatisticsDto(
    Guid Id,
    Guid SimulationId,
    int TotalOrders,
    int TotalFills,
    decimal TotalQuantity,
    decimal AverageSlippageBps,
    decimal TotalFees);
