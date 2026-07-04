using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class MarketMicrostructureModelRepository : BaseRepository<MarketMicrostructureModel, Guid>, IMarketMicrostructureModelRepository
{
    public MarketMicrostructureModelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
