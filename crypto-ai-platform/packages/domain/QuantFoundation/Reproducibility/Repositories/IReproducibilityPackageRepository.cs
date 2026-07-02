namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;

public interface IReproducibilityPackageRepository
{
    Task<ReproducibilityPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ReproducibilityPackage?> GetByExperimentRunIdAsync(Guid experimentRunId, CancellationToken cancellationToken = default);
    Task AddAsync(ReproducibilityPackage package, CancellationToken cancellationToken = default);
    Task UpdateAsync(ReproducibilityPackage package, CancellationToken cancellationToken = default);
}