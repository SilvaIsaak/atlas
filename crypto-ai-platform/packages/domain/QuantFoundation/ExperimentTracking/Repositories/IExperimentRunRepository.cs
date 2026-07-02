namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Repositories;

public interface IExperimentRunRepository
{
    Task<ExperimentRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExperimentRun>> GetByExperimentIdAsync(Guid experimentId, CancellationToken cancellationToken = default);
    Task AddAsync(ExperimentRun run, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExperimentRun run, CancellationToken cancellationToken = default);
}