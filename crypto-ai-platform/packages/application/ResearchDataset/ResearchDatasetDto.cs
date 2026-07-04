namespace CryptoAIPlatform.Application.ResearchDataset;

public record ResearchDatasetDto(
    Guid Id,
    string Name,
    string Description,
    Guid OwnerId,
    string Version,
    bool IsImmutable,
    DateTime CreatedAt);

public record DatasetVersionDto(
    Guid Id,
    Guid DatasetId,
    string Version,
    DateTime PeriodStart,
    DateTime PeriodEnd,
    IReadOnlyList<string> AssetSymbols,
    bool IsPublished,
    bool IsArchived,
    DateTime CreatedAt);
