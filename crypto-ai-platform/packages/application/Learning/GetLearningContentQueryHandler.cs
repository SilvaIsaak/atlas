using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Learning;

public class GetLearningContentQueryHandler : IRequestHandler<GetLearningContentQuery, GetLearningContentResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetLearningContentQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetLearningContentResponse> Handle(GetLearningContentQuery request, CancellationToken cancellationToken)
    {
        var content = await _dbContext.LearningContents
            .FirstOrDefaultAsync(c => c.Id == request.ContentId, cancellationToken);

        if (content == null)
        {
            throw new KeyNotFoundException("Learning content not found");
        }

        return new GetLearningContentResponse(
            content.Id,
            content.Title,
            content.Description,
            content.Type,
            content.Content,
            content.VideoUrl,
            content.ThumbnailUrl,
            content.Tags,
            content.Order,
            content.IsPublished,
            content.CreatedAt,
            content.UpdatedAt
        );
    }
}
