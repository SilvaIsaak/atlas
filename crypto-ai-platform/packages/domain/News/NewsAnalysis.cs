namespace CryptoAIPlatform.Domain.News;

public enum Sentiment
{
    Positive,
    Neutral,
    Negative
}

public record NewsAnalysis(
    Guid NewsId,
    Sentiment Sentiment,
    decimal ConfidenceScore
);
