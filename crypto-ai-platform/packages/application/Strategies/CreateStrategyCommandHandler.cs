using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Strategies;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Strategies;

public class CreateStrategyCommandHandler : IRequestHandler<CreateStrategyCommand, CreateStrategyResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateStrategyCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateStrategyResponse> Handle(CreateStrategyCommand request, CancellationToken cancellationToken)
    {
        var strategy = new Strategy
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Code = request.Code,
            AssetSymbol = request.AssetSymbol,
            Status = StrategyStatus.Draft,
            ResearchStudyId = request.ResearchStudyId
        };

        await _dbContext.Strategies.AddAsync(strategy, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateStrategyResponse
        {
            StrategyId = strategy.Id,
            Name = strategy.Name,
            Description = strategy.Description,
            Code = strategy.Code,
            AssetSymbol = strategy.AssetSymbol,
            Status = strategy.Status,
            ResearchStudyId = strategy.ResearchStudyId
        };
    }
}
