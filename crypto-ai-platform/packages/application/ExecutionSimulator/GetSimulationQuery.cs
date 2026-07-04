using MediatR;

namespace CryptoAIPlatform.Application.ExecutionSimulator;

public record GetSimulationQuery(Guid SimulationId) : IRequest<ExecutionSimulationDto?>;
public record GetSimulationsQuery() : IRequest<IReadOnlyCollection<ExecutionSimulationDto>>;
