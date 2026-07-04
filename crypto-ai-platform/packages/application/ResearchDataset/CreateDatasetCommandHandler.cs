using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;

namespace CryptoAIPlatform.Application.ResearchDataset;

public class CreateDatasetCommandHandler : IRequestHandler<CreateDatasetCommand, ResearchDatasetDto>
{
    private readonly IResearchDatasetService _datasetService;
    private readonly ILogger<CreateDatasetCommandHandler> _logger;

    public CreateDatasetCommandHandler(
        IResearchDatasetService datasetService,
        ILogger<CreateDatasetCommandHandler> logger)
    {
        _datasetService = datasetService;
        _logger = logger;
    }

    public async Task<ResearchDatasetDto> Handle(CreateDatasetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateDatasetCommand for name {Name}", request.Name);

        var dataset = await _datasetService.CreateDatasetAsync(
            tenantId: TenantId.Default,
            name: request.Name,
            description: request.Description,
            ownerId: request.OwnerId,
            initialVersion: request.InitialVersion,
            cancellationToken: cancellationToken);

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

public class CreateVersionCommandHandler : IRequestHandler<CreateVersionCommand, DatasetVersionDto>
{
    private readonly IResearchDatasetService _datasetService;
    private readonly ILogger<CreateVersionCommandHandler> _logger;

    public CreateVersionCommandHandler(
        IResearchDatasetService datasetService,
        ILogger<CreateVersionCommandHandler> logger)
    {
        _datasetService = datasetService;
        _logger = logger;
    }

    public async Task<DatasetVersionDto> Handle(CreateVersionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateVersionCommand for dataset {DatasetId}", request.DatasetId);

        var version = await _datasetService.CreateVersionAsync(
            tenantId: TenantId.Default,
            datasetId: request.DatasetId,
            version: request.Version,
            periodStart: request.PeriodStart,
            periodEnd: request.PeriodEnd,
            assetSymbols: request.AssetSymbols,
            cancellationToken: cancellationToken);

        return new DatasetVersionDto(
            Id: version.Id,
            DatasetId: version.DatasetId,
            Version: version.Version,
            PeriodStart: version.PeriodStart,
            PeriodEnd: version.PeriodEnd,
            AssetSymbols: version.AssetSymbols,
            IsPublished: version.IsPublished,
            IsArchived: version.IsArchived,
            CreatedAt: version.CreatedAt);
    }
}

public class PublishVersionCommandHandler : IRequestHandler<PublishVersionCommand, DatasetVersionDto>
{
    private readonly IResearchDatasetService _datasetService;
    private readonly ILogger<PublishVersionCommandHandler> _logger;

    public PublishVersionCommandHandler(
        IResearchDatasetService datasetService,
        ILogger<PublishVersionCommandHandler> logger)
    {
        _datasetService = datasetService;
        _logger = logger;
    }

    public async Task<DatasetVersionDto> Handle(PublishVersionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling PublishVersionCommand for version {VersionId}", request.VersionId);

        var version = await _datasetService.PublishVersionAsync(
            tenantId: TenantId.Default,
            datasetId: request.DatasetId,
            versionId: request.VersionId,
            cancellationToken: cancellationToken);

        return new DatasetVersionDto(
            Id: version.Id,
            DatasetId: version.DatasetId,
            Version: version.Version,
            PeriodStart: version.PeriodStart,
            PeriodEnd: version.PeriodEnd,
            AssetSymbols: version.AssetSymbols,
            IsPublished: version.IsPublished,
            IsArchived: version.IsArchived,
            CreatedAt: version.CreatedAt);
    }
}

public class ArchiveVersionCommandHandler : IRequestHandler<ArchiveVersionCommand, DatasetVersionDto>
{
    private readonly IResearchDatasetService _datasetService;
    private readonly ILogger<ArchiveVersionCommandHandler> _logger;

    public ArchiveVersionCommandHandler(
        IResearchDatasetService datasetService,
        ILogger<ArchiveVersionCommandHandler> logger)
    {
        _datasetService = datasetService;
        _logger = logger;
    }

    public async Task<DatasetVersionDto> Handle(ArchiveVersionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling ArchiveVersionCommand for version {VersionId}", request.VersionId);

        var version = await _datasetService.ArchiveVersionAsync(
            tenantId: TenantId.Default,
            datasetId: request.DatasetId,
            versionId: request.VersionId,
            cancellationToken: cancellationToken);

        return new DatasetVersionDto(
            Id: version.Id,
            DatasetId: version.DatasetId,
            Version: version.Version,
            PeriodStart: version.PeriodStart,
            PeriodEnd: version.PeriodEnd,
            AssetSymbols: version.AssetSymbols,
            IsPublished: version.IsPublished,
            IsArchived: version.IsArchived,
            CreatedAt: version.CreatedAt);
    }
}
