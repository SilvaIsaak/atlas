using MediatR;

namespace CryptoAIPlatform.Application.ResearchDataset;

public record CreateDatasetCommand(
    string Name,
    string Description,
    Guid OwnerId,
    string InitialVersion) : IRequest<ResearchDatasetDto>;

public record CreateVersionCommand(
    Guid DatasetId,
    string Version,
    DateTime PeriodStart,
    DateTime PeriodEnd,
    IReadOnlyList<string> AssetSymbols) : IRequest<DatasetVersionDto>;

public record PublishVersionCommand(
    Guid DatasetId,
    Guid VersionId) : IRequest<DatasetVersionDto>;

public record ArchiveVersionCommand(
    Guid DatasetId,
    Guid VersionId) : IRequest<DatasetVersionDto>;
