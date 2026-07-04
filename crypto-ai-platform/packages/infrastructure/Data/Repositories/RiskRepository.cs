using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Risk;
using CryptoAIPlatform.Domain.Risk.Repositories;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class RiskAssessmentRepository : IRiskAssessmentRepository
{
    private readonly ApplicationDbContext _context;

    public RiskAssessmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RiskAssessments
            .Include(a => a.Violations)
            .Include(a => a.Metrics)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<RiskAssessment>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.RiskAssessments
            .Include(a => a.Violations)
            .Include(a => a.Metrics)
            .Where(a => a.PortfolioId == portfolioId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RiskAssessment assessment, CancellationToken cancellationToken = default)
    {
        await _context.RiskAssessments.AddAsync(assessment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RiskAssessment assessment, CancellationToken cancellationToken = default)
    {
        _context.RiskAssessments.Update(assessment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class RiskLimitRepository : IRiskLimitRepository
{
    private readonly ApplicationDbContext _context;

    public RiskLimitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RiskLimit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RiskLimits.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<RiskLimit>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.RiskLimits
            .Where(r => r.PortfolioId == portfolioId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RiskLimit limit, CancellationToken cancellationToken = default)
    {
        await _context.RiskLimits.AddAsync(limit, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RiskLimit limit, CancellationToken cancellationToken = default)
    {
        _context.RiskLimits.Update(limit);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class RiskRuleRepository : IRiskRuleRepository
{
    private readonly ApplicationDbContext _context;

    public RiskRuleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RiskRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RiskRules.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<RiskRule>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.RiskRules
            .Where(r => r.PortfolioId == portfolioId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RiskRule rule, CancellationToken cancellationToken = default)
    {
        await _context.RiskRules.AddAsync(rule, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RiskRule rule, CancellationToken cancellationToken = default)
    {
        _context.RiskRules.Update(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class ExposureRepository : IExposureRepository
{
    private readonly ApplicationDbContext _context;

    public ExposureRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExposureProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ExposureProfiles
            .Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<ExposureProfile?> GetLatestByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.ExposureProfiles
            .Include(e => e.Items)
            .Where(e => e.PortfolioId == portfolioId)
            .OrderByDescending(e => e.GeneratedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(ExposureProfile profile, CancellationToken cancellationToken = default)
    {
        await _context.ExposureProfiles.AddAsync(profile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class PortfolioRiskRepository : IPortfolioRiskRepository
{
    private readonly ApplicationDbContext _context;

    public PortfolioRiskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PortfolioRiskSnapshot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.PortfolioRiskSnapshots
            .Include(p => p.VaRHistory)
            .Include(p => p.StressResults)
            .Include(p => p.Drawdown)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PortfolioRiskSnapshot?> GetLatestByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.PortfolioRiskSnapshots
            .Include(p => p.VaRHistory)
            .Include(p => p.StressResults)
            .Include(p => p.Drawdown)
            .Where(p => p.PortfolioId == portfolioId)
            .OrderByDescending(p => p.Timestamp)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(PortfolioRiskSnapshot snapshot, CancellationToken cancellationToken = default)
    {
        await _context.PortfolioRiskSnapshots.AddAsync(snapshot, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
