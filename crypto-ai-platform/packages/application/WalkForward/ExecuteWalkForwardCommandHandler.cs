using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.WalkForward;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.WalkForward;

public class ExecuteWalkForwardCommandHandler : IRequestHandler<ExecuteWalkForwardCommand, ExecuteWalkForwardResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public ExecuteWalkForwardCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExecuteWalkForwardResponse> Handle(ExecuteWalkForwardCommand request, CancellationToken cancellationToken)
    {
        var walkForward = await _dbContext.WalkForwards
            .FirstOrDefaultAsync(wf => wf.Id == request.WalkForwardId && wf.UserId == request.UserId, cancellationToken);

        if (walkForward == null)
        {
            throw new KeyNotFoundException("WalkForward not found");
        }

        walkForward.Status = WalkForwardStatus.Running;
        walkForward.StartedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        // Simulação de execução
        walkForward.Status = WalkForwardStatus.Completed;
        walkForward.CompletedAt = DateTime.UtcNow;
        walkForward.WindowResults = new List<WalkForwardWindowResult>
        {
            new WalkForwardWindowResult(
                1,
                walkForward.StartDate,
                walkForward.StartDate.AddDays(walkForward.TrainingWindowDays),
                walkForward.StartDate.AddDays(walkForward.TrainingWindowDays),
                walkForward.StartDate.AddDays(walkForward.TrainingWindowDays + walkForward.TestingWindowDays),
                8.5m,
                2.1m,
                1.7m,
                5.2m
            ),
            new WalkForwardWindowResult(
                2,
                walkForward.StartDate.AddDays(walkForward.TestingWindowDays),
                walkForward.StartDate.AddDays(walkForward.TestingWindowDays + walkForward.TrainingWindowDays),
                walkForward.StartDate.AddDays(walkForward.TestingWindowDays + walkForward.TrainingWindowDays),
                walkForward.StartDate.AddDays(walkForward.TestingWindowDays + walkForward.TrainingWindowDays + walkForward.TestingWindowDays),
                9.2m,
                1.8m,
                1.9m,
                4.8m
            )
        };
        walkForward.TotalOutOfSampleReturn = 3.9m;
        walkForward.AverageSharpeRatio = 1.8m;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ExecuteWalkForwardResponse
        {
            WalkForwardId = walkForward.Id,
            Status = walkForward.Status.ToString()
        };
    }
}