namespace CryptoAIPlatform.Application.Risk;

public record RiskAssessmentDto(Guid Id, Guid PortfolioId, Guid? OrderId, string Status, string Score, DateTime AssessedAt);
public record RiskLimitDto(Guid Id, Guid PortfolioId, string Name, string Type, decimal Threshold, string Severity, string Action, bool IsActive);
public record RiskRuleDto(Guid Id, Guid PortfolioId, string Name, string Expression, string Severity, string Action, bool IsActive);
public record ExposureProfileDto(Guid Id, Guid PortfolioId, DateTime GeneratedAt, decimal TotalExposure);
public record PortfolioRiskSnapshotDto(Guid Id, Guid PortfolioId, DateTime Timestamp, string Status, decimal MarginUsage, decimal Leverage);
