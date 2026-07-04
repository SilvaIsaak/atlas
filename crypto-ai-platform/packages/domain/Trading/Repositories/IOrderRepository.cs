namespace CryptoAIPlatform.Domain.Trading.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
}

public interface IPositionRepository
{
    Task<Position?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Position>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(Position position, CancellationToken cancellationToken = default);
    Task UpdateAsync(Position position, CancellationToken cancellationToken = default);
}

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Portfolio>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Portfolio portfolio, CancellationToken cancellationToken = default);
    Task UpdateAsync(Portfolio portfolio, CancellationToken cancellationToken = default);
}

public interface IRiskRepository
{
    Task<RiskProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<RiskProfile?> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task AddAsync(RiskProfile riskProfile, CancellationToken cancellationToken = default);
    Task UpdateAsync(RiskProfile riskProfile, CancellationToken cancellationToken = default);
}

public interface ITradeExecutionRepository
{
    Task<TradeExecution?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TradeExecution>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task AddAsync(TradeExecution tradeExecution, CancellationToken cancellationToken = default);
    Task UpdateAsync(TradeExecution tradeExecution, CancellationToken cancellationToken = default);
}
