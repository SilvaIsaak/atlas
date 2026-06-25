using MediatR;
using CryptoAIPlatform.Domain.Learning;

namespace CryptoAIPlatform.Application.Learning;

public record GetLearningContentsQuery : IRequest<List<GetLearningContentResponse>>
{
    public ContentType? Type { get; init; }
    public bool? OnlyPublished { get; init; } = true;
    public string? SearchTerm { get; init; }
}

public record GetLearningContentResponse(
    Guid ContentId,
    string Title,
    string Description,
    ContentType Type,
    string Content,
    string? VideoUrl,
    string? ThumbnailUrl,
    string? Tags,
    int Order,
    bool IsPublished,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
