using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Strategies;

public interface IStrategy
{
    Guid Id { get; }
    string Name { get; }
    Task<StrategySignal[]> GenerateSignalsAsync(TenantId tenantId, CancellationToken cancellationToken = default);
}

public interface ISignalGenerator { }
public interface IStrategyEngine { }
public interface IStrategyRepository { }
