using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;

namespace CryptoAIPlatform.Application.ExecutionSimulator;

public class GetSimulationQueryHandler : IRequestHandler<GetSimulationQuery, ExecutionSimulationDto?>
{
    private readonly IExecutionSimulationRepository _repository;
    private readonly ILogger<GetSimulationQueryHandler> _logger;

    public GetSimulationQueryHandler(
        IExecutionSimulationRepository repository,
        ILogger<GetSimulationQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ExecutionSimulationDto?> Handle(GetSimulationQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting simulation: {Id}", request.SimulationId);
        var sim = await _repository.GetByIdAsync(request.SimulationId, cancellationToken);
        if (sim is null) return null;
        return new ExecutionSimulationDto(
            Id: sim.Id,
            Name: sim.Name,
            MicrostructureModelId: sim.MicrostructureModelId,
            Status: sim.Status.ToString(),
            CreatedAt: sim.CreatedAt);
    }
}

public class GetSimulationsQueryHandler : IRequestHandler<GetSimulationsQuery, IReadOnlyCollection<ExecutionSimulationDto>>
{
    private readonly IExecutionSimulationRepository _repository;
    private readonly ILogger<GetSimulationsQueryHandler> _logger;

    public GetSimulationsQueryHandler(
        IExecutionSimulationRepository repository,
        ILogger<GetSimulationsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<ExecutionSimulationDto>> Handle(GetSimulationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all simulations");
        var sims = await _repository.GetAllAsync(TenantId.Default, cancellationToken);
        return sims.Select(s => new ExecutionSimulationDto(
            Id: s.Id,
            Name: s.Name,
            MicrostructureModelId: s.MicrostructureModelId,
            Status: s.Status.ToString(),
            CreatedAt: s.CreatedAt)).ToList();
    }
}
