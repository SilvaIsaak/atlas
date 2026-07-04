using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class ResearchDatasetRepository : BaseRepository<ResearchDataset, Guid>, IResearchDatasetRepository
{
    public ResearchDatasetRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
