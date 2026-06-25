using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Strategies;

public class ChangeStrategyStatusCommandHandler : IRequestHandler<ChangeStrategyStatusCommand, ChangeStrategyStatusResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public ChangeStrategyStatusCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ChangeStrategyStatusResponse> Handle(ChangeStrategyStatusCommand request, CancellationToken cancellationToken)
    {
        var strategy = await _dbContext.Strategies
            .FirstOrDefaultAsync(s => s.Id == request.StrategyId && s.UserId == request.UserId, cancellationToken);

        if (strategy == null)
        {
            throw new KeyNotFoundException("Strategy not found");
        }

        strategy.Status = request.NewStatus;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ChangeStrategyStatusResponse
        {
            StrategyId = strategy.Id,
            Status = strategy.Status
        };
    }
}
