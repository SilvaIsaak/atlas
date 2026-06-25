namespace CryptoAIPlatform.Domain.AIDecision;

public record AIModelConfig(
    Guid ConfigId,
    string ModelName,
    string Description,
    decimal RiskTolerance, // 0 (conservative) to 1 (aggressive)
    List<string> IndicatorsUsed,
    bool IsActive
);