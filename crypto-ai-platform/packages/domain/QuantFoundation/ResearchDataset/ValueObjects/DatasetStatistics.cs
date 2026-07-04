namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.ValueObjects;

public record DatasetStatistics(
    long RowCount,
    long ColumnCount,
    decimal TotalSizeBytes);
