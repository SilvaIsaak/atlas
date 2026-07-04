using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class AnomalyRepository : BaseRepository<Anomaly, Guid>, IAnomalyRepository
{
    public AnomalyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
