using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class DataQualityJobRepository : BaseRepository<DataQualityJob, Guid>, IDataQualityJobRepository
{
    public DataQualityJobRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
