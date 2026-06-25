using MediatR;
using CryptoAIPlatform.Domain.Research;

namespace CryptoAIPlatform.Application.Research;

public record GetResearchStudyQuery(Guid StudyId) : IRequest<ResearchStudy?>;

public class GetResearchStudyQueryHandler : IRequestHandler<GetResearchStudyQuery, ResearchStudy?>
{
    public async Task<ResearchStudy?> Handle(GetResearchStudyQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar busca do repositório
        return await Task.FromResult(new ResearchStudy
        {
            Id = request.StudyId,
            UserId = Guid.NewGuid(),
            Name = "Mock Study",
            Description = "Estud de exemplo",
            AssetSymbol = "BTC",
            IndicatorsUsed = new List<string> { "SMA", "RSI" },
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow
        });
    }
}
