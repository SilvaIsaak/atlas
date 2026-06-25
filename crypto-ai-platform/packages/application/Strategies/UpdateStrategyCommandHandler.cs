using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Strategies;

public class UpdateStrategyCommandHandler : IRequestHandler<UpdateStrategyCommand, UpdateStrategyResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateStrategyCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UpdateStrategyResponse> Handle(UpdateStrategyCommand request, CancellationToken cancellationToken)
    {
        var strategy = await _dbContext.Strategies
            .FirstOrDefaultAsync(s => s.Id == request.StrategyId && s.UserId == request.UserId, cancellationToken);

        if (strategy == null)
        {
            throw new KeyNotFoundException("Strategy not found");
        }

        strategy.Name = request.Name;
        strategy.Description = request.Description;
        strategy.Code = request.Code;
        strategy.AssetSymbol = request.AssetSymbol;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateStrategyResponse
        {
            StrategyId = strategy.Id,
            Name = strategy.Name,
            Description = strategy.Description,
            Code = strategy.Code,
            AssetSymbol = strategy.AssetSymbol,
            Status = strategy.Status
        };
    }
}
