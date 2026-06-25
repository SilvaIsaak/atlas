using MediatR;
using CryptoAIPlatform.Domain.News;

namespace CryptoAIPlatform.Application.News;

public record AnalyzeNewsSentimentCommand(News News) : IRequest<NewsAnalysis>;

public class AnalyzeNewsSentimentCommandHandler : IRequestHandler<AnalyzeNewsSentimentCommand, NewsAnalysis>
{
    private readonly INewsProvider _newsProvider;

    public AnalyzeNewsSentimentCommandHandler(INewsProvider newsProvider)
    {
        _newsProvider = newsProvider;
    }

    public async Task<NewsAnalysis> Handle(AnalyzeNewsSentimentCommand request, CancellationToken cancellationToken)
    {
        // For now, return mock sentiment analysis
        // TODO: Implement real sentiment analysis
        var sentiment = request.News.Title.ToLower().Contains("high") || 
                        request.News.Title.ToLower().Contains("success") || 
                        request.News.Title.ToLower().Contains("upgrade") 
                            ? Sentiment.Positive 
                            : Sentiment.Neutral;

        return await Task.FromResult(new NewsAnalysis(request.News.Id, sentiment, 0.85m));
    }
}
