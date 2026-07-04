using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;

namespace CryptoAIPlatform.Application.ExecutionSimulator;

public class CreateSimulationCommandHandler : IRequestHandler<CreateSimulationCommand, ExecutionSimulationDto>
{
    private readonly IExecutionSimulationRepository _repository;
    private readonly ILogger<CreateSimulationCommandHandler> _logger;

    public CreateSimulationCommandHandler(
        IExecutionSimulationRepository repository,
        ILogger<CreateSimulationCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ExecutionSimulationDto> Handle(CreateSimulationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating execution simulation: {Name}", request.Name);

        var sim = ExecutionSimulation.Create(
            id: Guid.NewGuid(),
            tenantId: TenantId.Default,
            name: request.Name,
            microstructureModelId: request.MicrostructureModelId);

        await _repository.AddAsync(sim, cancellationToken);

        return new ExecutionSimulationDto(
            Id: sim.Id,
            Name: sim.Name,
            MicrostructureModelId: sim.MicrostructureModelId,
            Status: sim.Status.ToString(),
            CreatedAt: sim.CreatedAt);
    }
}

public class RunSimulationCommandHandler : IRequestHandler<RunSimulationCommand, ExecutionSimulationDto>
{
    private readonly CryptoAIPlatform.Infrastructure.QuantFoundation.IExecutionSimulatorService _service;
    private readonly ILogger<RunSimulationCommandHandler> _logger;

    public RunSimulationCommandHandler(
        CryptoAIPlatform.Infrastructure.QuantFoundation.IExecutionSimulatorService service,
        ILogger<RunSimulationCommandHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<ExecutionSimulationDto> Handle(RunSimulationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Running simulation: {Id}", request.SimulationId);

        var sim = await _service.RunSimulationAsync(TenantId.Default, request.SimulationId, cancellationToken);

        return new ExecutionSimulationDto(
            Id: sim.Id,
            Name: sim.Name,
            MicrostructureModelId: sim.MicrostructureModelId,
            Status: sim.Status.ToString(),
            CreatedAt: sim.CreatedAt);
    }
}
