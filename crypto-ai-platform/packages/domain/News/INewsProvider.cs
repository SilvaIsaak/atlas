namespace CryptoAIPlatform.Domain.News;

public interface INewsProvider
{
    Task<List<News>> GetNewsAsync(string? assetSymbol = null, int limit = 50, CancellationToken cancellationToken = default);
    Task<NewsAnalysis> AnalyzeSentimentAsync(News news, CancellationToken cancellationToken = default);
}
