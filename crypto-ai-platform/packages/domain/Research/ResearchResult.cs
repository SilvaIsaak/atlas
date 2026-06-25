namespace CryptoAIPlatform.Domain.Research;

public record ResearchResult(
    decimal TotalReturn,
    decimal SharpeRatio,
    decimal MaxDrawdown,
    int NumberOfTrades,
    decimal WinRate
);
