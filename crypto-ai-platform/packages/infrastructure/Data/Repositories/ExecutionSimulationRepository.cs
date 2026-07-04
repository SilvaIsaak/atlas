using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class ExecutionSimulationRepository : BaseRepository<ExecutionSimulation, Guid>, IExecutionSimulationRepository
{
    public ExecutionSimulationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
