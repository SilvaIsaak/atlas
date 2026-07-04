using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;

namespace CryptoAIPlatform.Application.ResearchDataset;

public class GetDatasetQueryHandler : IRequestHandler<GetDatasetQuery, ResearchDatasetDto?>
{
    private readonly IResearchDatasetRepository _datasetRepository;
    private readonly ILogger<GetDatasetQueryHandler> _logger;

    public GetDatasetQueryHandler(
        IResearchDatasetRepository datasetRepository,
        ILogger<GetDatasetQueryHandler> logger)
    {
        _datasetRepository = datasetRepository;
        _logger = logger;
    }

    public async Task<ResearchDatasetDto?> Handle(GetDatasetQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetDatasetQuery for dataset {DatasetId}", request.DatasetId);

        var dataset = await _datasetRepository.GetByIdAsync(request.DatasetId, cancellationToken);
        if (dataset == null) return null;

        return new ResearchDatasetDto(
            Id: dataset.Id,
            Name: dataset.Name,
            Description: dataset.Description,
            OwnerId: dataset.OwnerId,
            Version: dataset.Version,
            IsImmutable: dataset.IsImmutable,
            CreatedAt: dataset.CreatedAt);
    }
}

public class GetDatasetHistoryQueryHandler : IRequestHandler<GetDatasetHistoryQuery, IReadOnlyList<ResearchDatasetDto>>
{
    private readonly IResearchDatasetService _datasetService;
    private readonly ILogger<GetDatasetHistoryQueryHandler> _logger;

    public GetDatasetHistoryQueryHandler(
        IResearchDatasetService datasetService,
        ILogger<GetDatasetHistoryQueryHandler> logger)
    {
        _datasetService = datasetService;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ResearchDatasetDto>> Handle(GetDatasetHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetDatasetHistoryQuery for owner {OwnerId}", request.OwnerId);

        var datasets = await _datasetService.GetDatasetHistoryAsync(
            tenantId: TenantId.Default,
            ownerId: request.OwnerId,
            cancellationToken: cancellationToken);

        return datasets
            .Select(d => new ResearchDatasetDto(
                Id: d.Id,
                Name: d.Name,
                Description: d.Description,
                OwnerId: d.OwnerId,
                Version: d.Version,
                IsImmutable: d.IsImmutable,
                CreatedAt: d.CreatedAt))
            .ToList();
    }
}

public class CompareVersionsQueryHandler : IRequestHandler<CompareVersionsQuery, IReadOnlyList<DatasetVersionDto>>
{
    private readonly IResearchDatasetService _datasetService;
    private readonly ILogger<CompareVersionsQueryHandler> _logger;

    public CompareVersionsQueryHandler(
        IResearchDatasetService datasetService,
        ILogger<CompareVersionsQueryHandler> logger)
    {
        _datasetService = datasetService;
        _logger = logger;
    }

    public async Task<IReadOnlyList<DatasetVersionDto>> Handle(CompareVersionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CompareVersionsQuery for versions {V1} and {V2}", request.VersionId1, request.VersionId2);

        var versions = await _datasetService.CompareVersionsAsync(
            tenantId: TenantId.Default,
            datasetId: request.DatasetId,
            versionId1: request.VersionId1,
            versionId2: request.VersionId2,
            cancellationToken: cancellationToken);

        return versions
            .Select(v => new DatasetVersionDto(
                Id: v.Id,
                DatasetId: v.DatasetId,
                Version: v.Version,
                PeriodStart: v.PeriodStart,
                PeriodEnd: v.PeriodEnd,
                AssetSymbols: v.AssetSymbols,
                IsPublished: v.IsPublished,
                IsArchived: v.IsArchived,
                CreatedAt: v.CreatedAt))
            .ToList();
    }
}
