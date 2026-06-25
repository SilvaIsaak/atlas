using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Learning;

public class GetLearningContentsQueryHandler : IRequestHandler<GetLearningContentsQuery, List<GetLearningContentResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetLearningContentsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetLearningContentResponse>> Handle(GetLearningContentsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.LearningContents.AsQueryable();

        if (request.Type.HasValue)
        {
            query = query.Where(c => c.Type == request.Type.Value);
        }

        if (request.OnlyPublished.HasValue && request.OnlyPublished.Value)
        {
            query = query.Where(c => c.IsPublished);
        }

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(c =>
                c.Title.Contains(request.SearchTerm) ||
                c.Description.Contains(request.SearchTerm) ||
                (c.Tags != null && c.Tags.Contains(request.SearchTerm)));
        }

        var contents = await query
            .OrderBy(c => c.Order)
            .ThenBy(c => c.CreatedAt)
            .Select(c => new GetLearningContentResponse(
                c.Id,
                c.Title,
                c.Description,
                c.Type,
                c.Content,
                c.VideoUrl,
                c.ThumbnailUrl,
                c.Tags,
                c.Order,
                c.IsPublished,
                c.CreatedAt,
                c.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return contents;
    }
}
