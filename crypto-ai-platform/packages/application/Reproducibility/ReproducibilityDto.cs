using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Application.Reproducibility;

public record ReproducibilityPackageDto(
    Guid Id,
    Guid ExperimentRunId,
    ReproducibilityPackageStatus Status,
    DateTime CreatedAt);

public record ExecutionManifestDto(
    Guid Id,
    Guid ReproducibilityPackageId,
    string ManifestJson,
    DateTime CreatedAt);
