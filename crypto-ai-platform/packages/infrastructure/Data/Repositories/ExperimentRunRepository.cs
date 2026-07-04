using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class ExperimentRunRepository : BaseRepository<ExperimentRun, Guid>, IExperimentRunRepository
{
    public ExperimentRunRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
