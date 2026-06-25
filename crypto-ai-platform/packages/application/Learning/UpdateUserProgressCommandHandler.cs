using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;
using CryptoAIPlatform.Domain.Learning;

namespace CryptoAIPlatform.Application.Learning;

public class UpdateUserProgressCommandHandler : IRequestHandler<UpdateUserProgressCommand, UpdateUserProgressResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateUserProgressCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UpdateUserProgressResponse> Handle(UpdateUserProgressCommand request, CancellationToken cancellationToken)
    {
        var progress = await _dbContext.UserLearningProgresses
            .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.ContentId == request.ContentId, cancellationToken);

        if (progress == null)
        {
            progress = new UserLearningProgress
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ContentId = request.ContentId,
                ProgressPercentage = request.ProgressPercentage,
                IsCompleted = request.IsCompleted,
                CompletedAt = request.IsCompleted ? DateTime.UtcNow : null
            };
            await _dbContext.UserLearningProgresses.AddAsync(progress, cancellationToken);
        }
        else
        {
            progress.ProgressPercentage = request.ProgressPercentage;
            progress.IsCompleted = request.IsCompleted;
            if (request.IsCompleted && progress.CompletedAt == null)
            {
                progress.CompletedAt = DateTime.UtcNow;
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateUserProgressResponse(
            progress.Id,
            progress.ProgressPercentage,
            progress.IsCompleted,
            progress.CompletedAt
        );
    }
}
