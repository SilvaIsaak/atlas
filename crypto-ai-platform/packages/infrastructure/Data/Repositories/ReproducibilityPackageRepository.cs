using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class ReproducibilityPackageRepository : BaseRepository<ReproducibilityPackage, Guid>, IReproducibilityPackageRepository
{
    public ReproducibilityPackageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
