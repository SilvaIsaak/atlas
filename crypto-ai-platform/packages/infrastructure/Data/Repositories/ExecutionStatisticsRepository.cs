using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class ExecutionStatisticsRepository : BaseRepository<ExecutionStatistics, Guid>, IExecutionStatisticsRepository
{
    public ExecutionStatisticsRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IReadOnlyCollection<ExecutionStatistics>> GetBySimulationIdAsync(Guid simulationId, TenantId tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ExecutionStatistics
            .Where(x => x.SimulationId == simulationId && x.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}
