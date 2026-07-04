using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class FeatureLineageRepository : BaseRepository<FeatureLineage, Guid>, IFeatureLineageRepository
{
    public FeatureLineageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
