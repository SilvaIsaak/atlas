using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;
using CryptoAIPlatform.Infrastructure.QuantFoundation;

namespace CryptoAIPlatform.Application.Reproducibility;

public class CreateReproducibilityPackageCommandHandler : IRequestHandler<CreateReproducibilityPackageCommand, ReproducibilityPackageDto>
{
    private readonly IReproducibilityService _reproducibilityService;
    private readonly ILogger<CreateReproducibilityPackageCommandHandler> _logger;

    public CreateReproducibilityPackageCommandHandler(
        IReproducibilityService reproducibilityService,
        ILogger<CreateReproducibilityPackageCommandHandler> logger)
    {
        _reproducibilityService = reproducibilityService;
        _logger = logger;
    }

    public async Task<ReproducibilityPackageDto> Handle(CreateReproducibilityPackageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating reproducibility package for Experiment Run {RunId}", request.ExperimentRunId);
        
        var package = await _reproducibilityService.GeneratePackageAsync(
            TenantId.Default,
            request.ExperimentRunId,
            request.EnvironmentInfo,
            cancellationToken);

        return new ReproducibilityPackageDto(
            Id: package.Id,
            ExperimentRunId: package.ExperimentRunId,
            Status: package.Status,
            CreatedAt: package.CreatedAt);
    }
}

public class GenerateManifestCommandHandler : IRequestHandler<GenerateManifestCommand, ExecutionManifestDto>
{
    private readonly IReproducibilityService _reproducibilityService;
    private readonly ILogger<GenerateManifestCommandHandler> _logger;

    public GenerateManifestCommandHandler(
        IReproducibilityService reproducibilityService,
        ILogger<GenerateManifestCommandHandler> logger)
    {
        _reproducibilityService = reproducibilityService;
        _logger = logger;
    }

    public async Task<ExecutionManifestDto> Handle(GenerateManifestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generating manifest for package {PackageId}", request.PackageId);
        
        var manifest = await _reproducibilityService.GenerateManifestAsync(
            TenantId.Default,
            request.PackageId,
            cancellationToken);

        return new ExecutionManifestDto(
            Id: manifest.Id,
            ReproducibilityPackageId: manifest.ReproducibilityPackageId,
            ManifestJson: manifest.ManifestJson,
            CreatedAt: manifest.CreatedAt);
    }
}

public class ValidatePackageCommandHandler : IRequestHandler<ValidatePackageCommand, bool>
{
    private readonly IReproducibilityService _reproducibilityService;
    private readonly ILogger<ValidatePackageCommandHandler> _logger;

    public ValidatePackageCommandHandler(
        IReproducibilityService reproducibilityService,
        ILogger<ValidatePackageCommandHandler> logger)
    {
        _reproducibilityService = reproducibilityService;
        _logger = logger;
    }

    public async Task<bool> Handle(ValidatePackageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating package {PackageId}", request.PackageId);
        
        return await _reproducibilityService.ValidatePackageAsync(
            TenantId.Default,
            request.PackageId,
            cancellationToken);
    }
}

public class CalculateChecksumCommandHandler : IRequestHandler<CalculateChecksumCommand, string>
{
    private readonly IReproducibilityService _reproducibilityService;
    private readonly ILogger<CalculateChecksumCommandHandler> _logger;

    public CalculateChecksumCommandHandler(
        IReproducibilityService reproducibilityService,
        ILogger<CalculateChecksumCommandHandler> logger)
    {
        _reproducibilityService = reproducibilityService;
        _logger = logger;
    }

    public async Task<string> Handle(CalculateChecksumCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Calculating checksum for package {PackageId} with algorithm {Algorithm}", request.PackageId, request.Algorithm);
        
        return await _reproducibilityService.CalculateChecksumAsync(
            TenantId.Default,
            request.PackageId,
            request.Algorithm,
            cancellationToken);
    }
}

public class ExportManifestCommandHandler : IRequestHandler<ExportManifestCommand, string>
{
    private readonly IReproducibilityService _reproducibilityService;
    private readonly ILogger<ExportManifestCommandHandler> _logger;

    public ExportManifestCommandHandler(
        IReproducibilityService reproducibilityService,
        ILogger<ExportManifestCommandHandler> logger)
    {
        _reproducibilityService = reproducibilityService;
        _logger = logger;
    }

    public async Task<string> Handle(ExportManifestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Exporting manifest for package {PackageId} to {ExportPath}", request.PackageId, request.ExportPath);
        
        return await _reproducibilityService.ExportManifestAsync(
            TenantId.Default,
            request.PackageId,
            request.ExportPath,
            cancellationToken);
    }
}
