using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

namespace CryptoAIPlatform.Application.ExperimentTracking;

public class CreateExperimentCommandHandler : IRequestHandler<CreateExperimentCommand, ExperimentDto>
{
    private readonly IExperimentTrackingService _experimentTrackingService;
    private readonly ILogger<CreateExperimentCommandHandler> _logger;

    public CreateExperimentCommandHandler(
        IExperimentTrackingService experimentTrackingService,
        ILogger<CreateExperimentCommandHandler> logger)
    {
        _experimentTrackingService = experimentTrackingService;
        _logger = logger;
    }

    public async Task<ExperimentDto> Handle(CreateExperimentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateExperimentCommand for name {Name}", request.Name);
        
        var experiment = await _experimentTrackingService.CreateExperimentAsync(
            tenantId: TenantId.Default, // In real scenario, get from current user context
            name: request.Name,
            description: request.Description,
            type: request.Type,
            ownerId: request.OwnerId,
            parameters: request.Parameters,
            cancellationToken: cancellationToken);

        return new ExperimentDto(
            Id: experiment.Id,
            Name: experiment.Name,
            Description: experiment.Description,
            Type: experiment.Type,
            OwnerId: experiment.OwnerId,
            CreatedAt: experiment.CreatedAt);
    }
}
