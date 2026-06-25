namespace CryptoAIPlatform.Domain.RiskManagement;

public record RiskAlert(
    Guid AlertId,
    string RuleName,
    RiskAlertLevel Level,
    string Message,
    DateTime CreatedAt,
    bool Acknowledged
);