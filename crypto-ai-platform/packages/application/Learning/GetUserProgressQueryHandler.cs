using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Learning;

public class GetUserProgressQueryHandler : IRequestHandler<GetUserProgressQuery, List<GetUserProgressResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetUserProgressQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetUserProgressResponse>> Handle(GetUserProgressQuery request, CancellationToken cancellationToken)
    {
        var progressList = await _dbContext.UserLearningProgresses
            .Where(p => p.UserId == request.UserId)
            .Select(p => new GetUserProgressResponse(
                p.Id,
                p.ContentId,
                p.ProgressPercentage,
                p.IsCompleted,
                p.CompletedAt,
                p.CreatedAt,
                p.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return progressList;
    }
}
