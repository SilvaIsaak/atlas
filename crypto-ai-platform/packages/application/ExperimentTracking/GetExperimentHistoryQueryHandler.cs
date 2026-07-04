using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

namespace CryptoAIPlatform.Application.ExperimentTracking;

public class GetExperimentHistoryQueryHandler : IRequestHandler<GetExperimentHistoryQuery, IReadOnlyList<ExperimentDto>>
{
    private readonly IExperimentTrackingService _experimentTrackingService;
    private readonly ILogger<GetExperimentHistoryQueryHandler> _logger;

    public GetExperimentHistoryQueryHandler(
        IExperimentTrackingService experimentTrackingService,
        ILogger<GetExperimentHistoryQueryHandler> logger)
    {
        _experimentTrackingService = experimentTrackingService;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ExperimentDto>> Handle(GetExperimentHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetExperimentHistoryQuery for owner {OwnerId}", request.OwnerId);
        
        var experiments = await _experimentTrackingService.GetExperimentHistoryAsync(
            tenantId: TenantId.Default,
            ownerId: request.OwnerId,
            cancellationToken: cancellationToken);

        return experiments
            .Select(exp => new ExperimentDto(
                Id: exp.Id,
                Name: exp.Name,
                Description: exp.Description,
                Type: exp.Type,
                OwnerId: exp.OwnerId,
                CreatedAt: exp.CreatedAt))
            .ToList();
    }
}
