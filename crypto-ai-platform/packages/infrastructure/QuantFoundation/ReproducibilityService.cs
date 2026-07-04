using Microsoft.Extensions.Logging;
using System.Text.Json;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Events;

namespace CryptoAIPlatform.Infrastructure.QuantFoundation;

public interface IReproducibilityService
{
    Task<ReproducibilityPackage> GeneratePackageAsync(TenantId tenantId, Guid experimentRunId, EnvironmentInfo environmentInfo, CancellationToken cancellationToken = default);
    Task<ExecutionManifest> GenerateManifestAsync(TenantId tenantId, Guid packageId, CancellationToken cancellationToken = default);
    Task<bool> ValidatePackageAsync(TenantId tenantId, Guid packageId, CancellationToken cancellationToken = default);
    Task<string> CalculateChecksumAsync(TenantId tenantId, Guid packageId, string algorithm, CancellationToken cancellationToken = default);
    Task<string> ExportManifestAsync(TenantId tenantId, Guid packageId, string exportPath, CancellationToken cancellationToken = default);
}

public class ReproducibilityService : IReproducibilityService
{
    private readonly IReproducibilityPackageRepository _packageRepository;
    private readonly IExecutionManifestRepository _manifestRepository;
    private readonly ILogger<ReproducibilityService> _logger;

    public ReproducibilityService(
        IReproducibilityPackageRepository packageRepository,
        IExecutionManifestRepository manifestRepository,
        ILogger<ReproducibilityService> logger)
    {
        _packageRepository = packageRepository;
        _manifestRepository = manifestRepository;
        _logger = logger;
    }

    public async Task<ReproducibilityPackage> GeneratePackageAsync(
        TenantId tenantId,
        Guid experimentRunId,
        EnvironmentInfo environmentInfo,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Generating reproducibility package for Experiment Run {RunId}", experimentRunId);
        
        var package = ReproducibilityPackage.Create(
            id: Guid.NewGuid(),
            tenantId: tenantId,
            experimentRunId: experimentRunId,
            environmentInfo: environmentInfo);

        package.Start();

        await _packageRepository.AddAsync(package, cancellationToken);

        return package;
    }

    public async Task<ExecutionManifest> GenerateManifestAsync(
        TenantId tenantId,
        Guid packageId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Generating manifest for package {PackageId}", packageId);
        
        var package = await _packageRepository.GetByIdAsync(packageId, cancellationToken);
        if (package == null)
            throw new KeyNotFoundException($"Reproducibility package {packageId} not found.");

        var manifestJson = JsonSerializer.Serialize(new
        {
            PackageId = package.Id,
            ExperimentRunId = package.ExperimentRunId,
            CreatedAt = package.CreatedAt,
            EnvironmentInfo = package.EnvironmentInfo
        });

        var hash = new ManifestHash("SHA256", "placeholder");
        var fingerprint = new ExecutionFingerprint("placeholder");

        var manifest = ExecutionManifest.Create(
            id: Guid.NewGuid(),
            tenantId: tenantId,
            reproducibilityPackageId: packageId,
            manifestJson: manifestJson,
            hash: hash,
            fingerprint: fingerprint);

        package.AddExecutionManifest(manifest);

        await _manifestRepository.AddAsync(manifest, cancellationToken);

        return manifest;
    }

    public async Task<bool> ValidatePackageAsync(
        TenantId tenantId,
        Guid packageId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Validating package {PackageId}", packageId);
        
        var package = await _packageRepository.GetByIdAsync(packageId, cancellationToken);
        if (package == null)
            throw new KeyNotFoundException($"Reproducibility package {packageId} not found.");

        package.Verify();
        await _packageRepository.UpdateAsync(package, cancellationToken);

        return true;
    }

    public async Task<string> CalculateChecksumAsync(
        TenantId tenantId,
        Guid packageId,
        string algorithm,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Calculating checksum for package {PackageId} with algorithm {Algorithm}", packageId, algorithm);
        
        var package = await _packageRepository.GetByIdAsync(packageId, cancellationToken);
        if (package == null)
            throw new KeyNotFoundException($"Reproducibility package {packageId} not found.");

        var checksum = new PackageChecksum(algorithm, "placeholder");
        package.SetChecksum(checksum);

        await _packageRepository.UpdateAsync(package, cancellationToken);

        return checksum.Value;
    }

    public async Task<string> ExportManifestAsync(
        TenantId tenantId,
        Guid packageId,
        string exportPath,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Exporting manifest for package {PackageId} to {ExportPath}", packageId, exportPath);
        
        var package = await _packageRepository.GetByIdAsync(packageId, cancellationToken);
        if (package == null)
            throw new KeyNotFoundException($"Reproducibility package {packageId} not found.");

        var manifest = package.ExecutionManifests.FirstOrDefault();
        if (manifest == null)
            throw new InvalidOperationException($"No manifest found for package {packageId}.");

        await File.WriteAllTextAsync(exportPath, manifest.ManifestJson, cancellationToken);

        return exportPath;
    }
}
