using MediatR;

namespace CryptoAIPlatform.Application.Reproducibility;

public record GetReproducibilityPackageQuery(Guid PackageId) : IRequest<ReproducibilityPackageDto?>;
public record GetReproducibilityPackagesByRunIdQuery(Guid ExperimentRunId) : IRequest<IReadOnlyList<ReproducibilityPackageDto>>;
public record GetExecutionManifestQuery(Guid ManifestId) : IRequest<ExecutionManifestDto?>;
