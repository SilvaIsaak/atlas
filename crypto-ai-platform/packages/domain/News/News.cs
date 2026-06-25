using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.News;

public class News : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public List<string> RelatedAssets { get; set; } = new List<string>();
}
