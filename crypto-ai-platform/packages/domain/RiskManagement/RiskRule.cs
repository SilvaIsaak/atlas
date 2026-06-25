namespace CryptoAIPlatform.Domain.RiskManagement;

public record RiskRule(
    Guid RuleId,
    string Name,
    string Description,
    decimal MaxDrawdownPercent,
    decimal MaxPositionSizePercent,
    decimal MaxDailyLossPercent,
    bool IsActive
);