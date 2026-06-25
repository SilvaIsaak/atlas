using MediatR;
using CryptoAIPlatform.Domain.Research;

namespace CryptoAIPlatform.Application.Research;

public record GetAllResearchStudiesQuery(Guid UserId) : IRequest<List<ResearchStudy>>;

public class GetAllResearchStudiesQueryHandler : IRequestHandler<GetAllResearchStudiesQuery, List<ResearchStudy>>
{
    public async Task<List<ResearchStudy>> Handle(GetAllResearchStudiesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar busca do repositório
        return await Task.FromResult(new List<ResearchStudy>
        {
            new ResearchStudy
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = "Estud 1",
                Description = "Primeiro estudo",
                AssetSymbol = "BTC",
                IndicatorsUsed = new List<string> { "SMA" },
                StartDate = DateTime.UtcNow.AddMonths(-12),
                EndDate = DateTime.UtcNow
            },
            new ResearchStudy
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = "Estud 2",
                Description = "Segundo estudo",
                AssetSymbol = "ETH",
                IndicatorsUsed = new List<string> { "RSI", "MACD" },
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EndDate = DateTime.UtcNow
            }
        });
    }
}
