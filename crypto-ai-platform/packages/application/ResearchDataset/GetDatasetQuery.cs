using MediatR;

namespace CryptoAIPlatform.Application.ResearchDataset;

public record GetDatasetQuery(Guid DatasetId) : IRequest<ResearchDatasetDto?>;

public record GetDatasetHistoryQuery(Guid OwnerId) : IRequest<IReadOnlyList<ResearchDatasetDto>>;

public record CompareVersionsQuery(
    Guid DatasetId,
    Guid VersionId1,
    Guid VersionId2) : IRequest<IReadOnlyList<DatasetVersionDto>>;
