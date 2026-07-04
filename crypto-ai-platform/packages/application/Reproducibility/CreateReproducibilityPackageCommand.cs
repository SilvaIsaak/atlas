using MediatR;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

namespace CryptoAIPlatform.Application.Reproducibility;

public record CreateReproducibilityPackageCommand(
    Guid ExperimentRunId,
    EnvironmentInfo EnvironmentInfo) : IRequest<ReproducibilityPackageDto>;

public record GenerateManifestCommand(
    Guid PackageId) : IRequest<ExecutionManifestDto>;

public record ValidatePackageCommand(
    Guid PackageId) : IRequest<bool>;

public record CalculateChecksumCommand(
    Guid PackageId,
    string Algorithm) : IRequest<string>;

public record ExportManifestCommand(
    Guid PackageId,
    string ExportPath) : IRequest<string>;
