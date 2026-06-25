using MediatR;
using CryptoAIPlatform.Domain.Research;

namespace CryptoAIPlatform.Application.Research;

public record ExecuteResearchStudyCommand(Guid StudyId) : IRequest<ResearchResult>;

public class ExecuteResearchStudyCommandHandler : IRequestHandler<ExecuteResearchStudyCommand, ResearchResult>
{
    private readonly IResearchEngineService _researchEngineService;

    public ExecuteResearchStudyCommandHandler(IResearchEngineService researchEngineService)
    {
        _researchEngineService = researchEngineService;
    }

    public async Task<ResearchResult> Handle(ExecuteResearchStudyCommand request, CancellationToken cancellationToken)
    {
        // TODO: Buscar o estudo do repositório
        var mockStudy = new ResearchStudy
        {
            Id = request.StudyId,
            UserId = Guid.NewGuid(),
            Name = "Mock Study",
            AssetSymbol = "BTC",
            IndicatorsUsed = new List<string> { "SMA", "RSI" },
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow
        };

        // Mock result
        return await Task.FromResult(new ResearchResult(
            TotalReturn: 0.25m,
            SharpeRatio: 1.5m,
            MaxDrawdown: -0.10m,
            NumberOfTrades: 50,
            WinRate: 0.65m
        ));
    }
}
