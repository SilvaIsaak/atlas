using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Learning;

public class LearningContent : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ContentType Type { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Tags { get; set; }
    public int Order { get; set; }
    public bool IsPublished { get; set; }
}
