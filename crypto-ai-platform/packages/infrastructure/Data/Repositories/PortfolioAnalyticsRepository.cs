using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.PortfolioAnalytics;
using CryptoAIPlatform.Domain.PortfolioAnalytics.Repositories;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class PortfolioAnalyticsRepository : IPortfolioAnalyticsRepository
{
    private readonly ApplicationDbContext _context;

    public PortfolioAnalyticsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PortfolioPerformanceReport?> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PortfolioPerformanceReport>()
            .Include(x => x.Snapshots)
            .FirstOrDefaultAsync(x => x.PortfolioId == portfolioId, cancellationToken);
    }

    public async Task<PerformanceSnapshot?> GetLatestSnapshotByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PerformanceSnapshot>()
            .Include(x => x.EquityCurve)
            .Include(x => x.Drawdowns)
            .Include(x => x.Benchmark)
            .Where(x => x.PortfolioId == portfolioId)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(PortfolioPerformanceReport analytics, CancellationToken cancellationToken = default)
    {
        await _context.Set<PortfolioPerformanceReport>().AddAsync(analytics, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(PerformanceSnapshot snapshot, CancellationToken cancellationToken = default)
    {
        await _context.Set<PerformanceSnapshot>().AddAsync(snapshot, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PortfolioPerformanceReport analytics, CancellationToken cancellationToken = default)
    {
        _context.Set<PortfolioPerformanceReport>().Update(analytics);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
