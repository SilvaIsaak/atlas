namespace CryptoAIPlatform.Domain.QuantFoundation.Enums;

public enum AnomalyType
{
    MissingCandle,
    Gap,
    Spike,
    Outlier,
    Duplicate,
    Corrupt,
    InvalidTimestamp,
    VolumeInconsistent
}