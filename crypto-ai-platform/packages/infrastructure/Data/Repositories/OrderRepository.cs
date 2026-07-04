using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Trading;
using CryptoAIPlatform.Domain.Trading.Repositories;

namespace CryptoAIPlatform.Infrastructure.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Fills)
            .Include(o => o.Fees)
            .Include(o => o.StatusHistory)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Where(o => o.PortfolioId.Value == portfolioId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class PositionRepository : IPositionRepository
{
    private readonly ApplicationDbContext _context;

    public PositionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Position?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .Include(p => p.Legs)
            .Include(p => p.PnLs)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Position>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .Where(p => p.PortfolioId.Value == portfolioId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Position position, CancellationToken cancellationToken = default)
    {
        await _context.Positions.AddAsync(position, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Position position, CancellationToken cancellationToken = default)
    {
        _context.Positions.Update(position);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDbContext _context;

    public PortfolioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .Include(p => p.Assets)
            .Include(p => p.Balances)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Portfolio>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Portfolio portfolio, CancellationToken cancellationToken = default)
    {
        await _context.Portfolios.AddAsync(portfolio, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Portfolio portfolio, CancellationToken cancellationToken = default)
    {
        _context.Portfolios.Update(portfolio);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class RiskRepository : IRiskRepository
{
    private readonly ApplicationDbContext _context;

    public RiskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RiskProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RiskProfiles.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<RiskProfile?> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.RiskProfiles.FirstOrDefaultAsync(r => r.PortfolioId == portfolioId, cancellationToken);
    }

    public async Task AddAsync(RiskProfile riskProfile, CancellationToken cancellationToken = default)
    {
        await _context.RiskProfiles.AddAsync(riskProfile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RiskProfile riskProfile, CancellationToken cancellationToken = default)
    {
        _context.RiskProfiles.Update(riskProfile);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class TradeExecutionRepository : ITradeExecutionRepository
{
    private readonly ApplicationDbContext _context;

    public TradeExecutionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TradeExecution?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TradeExecutions
            .Include(t => t.Fills)
            .Include(t => t.Fees)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TradeExecution>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _context.TradeExecutions
            .Where(t => t.OrderId == orderId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TradeExecution tradeExecution, CancellationToken cancellationToken = default)
    {
        await _context.TradeExecutions.AddAsync(tradeExecution, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TradeExecution tradeExecution, CancellationToken cancellationToken = default)
    {
        _context.TradeExecutions.Update(tradeExecution);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
