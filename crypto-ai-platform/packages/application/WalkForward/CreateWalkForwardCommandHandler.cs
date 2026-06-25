using MediatR;
using CryptoAIPlatform.Domain.WalkForward;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.WalkForward;

public class CreateWalkForwardCommandHandler : IRequestHandler<CreateWalkForwardCommand, CreateWalkForwardResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateWalkForwardCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateWalkForwardResponse> Handle(CreateWalkForwardCommand request, CancellationToken cancellationToken)
    {
        var walkForward = new WalkForward
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            StrategyId = request.StrategyId,
            Name = request.Name,
            Description = request.Description,
            AssetSymbol = request.AssetSymbol,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TrainingWindowDays = request.TrainingWindowDays,
            TestingWindowDays = request.TestingWindowDays,
            InitialCapital = request.InitialCapital,
            Status = WalkForwardStatus.Pending
        };

        await _dbContext.WalkForwards.AddAsync(walkForward, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateWalkForwardResponse
        {
            WalkForwardId = walkForward.Id,
            StrategyId = walkForward.StrategyId,
            Name = walkForward.Name,
            Status = walkForward.Status
        };
    }
}