using MediatR;
using CryptoAIPlatform.Domain.News;
using NewsModel = CryptoAIPlatform.Domain.News.News;

namespace CryptoAIPlatform.Application.News;

public record GetNewsQuery(string? AssetSymbol = null, int Limit = 50) : IRequest<List<NewsModel>>;

public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, List<NewsModel>>
{
    private readonly INewsProvider _newsProvider;

    public GetNewsQueryHandler(INewsProvider newsProvider)
    {
        _newsProvider = newsProvider;
    }

    public async Task<List<NewsModel>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
    {
        // For now, return mock data
        // TODO: Implement real news provider integration
        return new List<NewsModel>
        {
            new NewsModel
            {
                Title = "Bitcoin Reaches New All-Time High",
                Content = "Bitcoin has reached a new all-time high of $100,000, driven by increased institutional adoption.",
                Source = "CryptoNews",
                Url = "https://example.com/bitcoin-all-time-high",
                PublishedAt = DateTime.UtcNow,
                RelatedAssets = new List<string> { "BTC", "ETH" }
            },
            new NewsModel
            {
                Title = "Ethereum Launches New Upgrade",
                Content = "Ethereum has successfully launched its latest upgrade, improving network scalability and security.",
                Source = "Ethereum News",
                Url = "https://example.com/ethereum-upgrade",
                PublishedAt = DateTime.UtcNow.AddHours(-2),
                RelatedAssets = new List<string> { "ETH" }
            }
        };
    }
}
