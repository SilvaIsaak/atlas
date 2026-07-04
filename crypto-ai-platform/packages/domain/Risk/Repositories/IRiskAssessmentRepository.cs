namespace CryptoAIPlatform.Domain.Risk.Repositories;

public interface IRiskAssessmentRepository
{
    Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<RiskAssessment>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(RiskAssessment assessment, CancellationToken cancellationToken = default);
    Task UpdateAsync(RiskAssessment assessment, CancellationToken cancellationToken = default);
}

public interface IRiskLimitRepository
{
    Task<RiskLimit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<RiskLimit>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(RiskLimit limit, CancellationToken cancellationToken = default);
    Task UpdateAsync(RiskLimit limit, CancellationToken cancellationToken = default);
}

public interface IRiskRuleRepository
{
    Task<RiskRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<RiskRule>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(RiskRule rule, CancellationToken cancellationToken = default);
    Task UpdateAsync(RiskRule rule, CancellationToken cancellationToken = default);
}

public interface IExposureRepository
{
    Task<ExposureProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ExposureProfile?> GetLatestByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(ExposureProfile profile, CancellationToken cancellationToken = default);
}

public interface IPortfolioRiskRepository
{
    Task<PortfolioRiskSnapshot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PortfolioRiskSnapshot?> GetLatestByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(PortfolioRiskSnapshot snapshot, CancellationToken cancellationToken = default);
}
