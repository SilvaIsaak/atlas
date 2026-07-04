using MediatR;

namespace CryptoAIPlatform.Application.ExecutionSimulator;

public record CreateSimulationCommand(
    string Name,
    Guid? MicrostructureModelId = null) : IRequest<ExecutionSimulationDto>;

public record RunSimulationCommand(
    Guid SimulationId) : IRequest<ExecutionSimulationDto>;
