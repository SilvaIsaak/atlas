using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;

namespace CryptoAIPlatform.Application.Reproducibility;

public class GetReproducibilityPackageQueryHandler : IRequestHandler<GetReproducibilityPackageQuery, ReproducibilityPackageDto?>
{
    private readonly IReproducibilityPackageRepository _repository;
    private readonly ILogger<GetReproducibilityPackageQueryHandler> _logger;

    public GetReproducibilityPackageQueryHandler(
        IReproducibilityPackageRepository repository,
        ILogger<GetReproducibilityPackageQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ReproducibilityPackageDto?> Handle(GetReproducibilityPackageQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reproducibility package {PackageId}", request.PackageId);
        
        var package = await _repository.GetByIdAsync(request.PackageId, cancellationToken);
        
        return package == null ? null : new ReproducibilityPackageDto(
            Id: package.Id,
            ExperimentRunId: package.ExperimentRunId,
            Status: package.Status,
            CreatedAt: package.CreatedAt);
    }
}

public class GetReproducibilityPackagesByRunIdQueryHandler : IRequestHandler<GetReproducibilityPackagesByRunIdQuery, IReadOnlyList<ReproducibilityPackageDto>>
{
    private readonly IReproducibilityPackageRepository _repository;
    private readonly ILogger<GetReproducibilityPackagesByRunIdQueryHandler> _logger;

    public GetReproducibilityPackagesByRunIdQueryHandler(
        IReproducibilityPackageRepository repository,
        ILogger<GetReproducibilityPackagesByRunIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ReproducibilityPackageDto>> Handle(GetReproducibilityPackagesByRunIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reproducibility packages for experiment run {RunId}", request.ExperimentRunId);
        
        var packages = await _repository.GetByExperimentRunIdAsync(TenantId.Default, request.ExperimentRunId, cancellationToken);
        
        return packages.Select(p => new ReproducibilityPackageDto(
            Id: p.Id,
            ExperimentRunId: p.ExperimentRunId,
            Status: p.Status,
            CreatedAt: p.CreatedAt)).ToList();
    }
}

public class GetExecutionManifestQueryHandler : IRequestHandler<GetExecutionManifestQuery, ExecutionManifestDto?>
{
    private readonly IExecutionManifestRepository _repository;
    private readonly ILogger<GetExecutionManifestQueryHandler> _logger;

    public GetExecutionManifestQueryHandler(
        IExecutionManifestRepository repository,
        ILogger<GetExecutionManifestQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ExecutionManifestDto?> Handle(GetExecutionManifestQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting execution manifest {ManifestId}", request.ManifestId);
        
        var manifest = await _repository.GetByIdAsync(request.ManifestId, cancellationToken);
        
        return manifest == null ? null : new ExecutionManifestDto(
            Id: manifest.Id,
            ReproducibilityPackageId: manifest.ReproducibilityPackageId,
            ManifestJson: manifest.ManifestJson,
            CreatedAt: manifest.CreatedAt);
    }
}
