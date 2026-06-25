using MediatR;
using CryptoAIPlatform.Domain.Research;

namespace CryptoAIPlatform.Application.Research;

public record CreateResearchStudyCommand(
    Guid UserId,
    string Name,
    string Description,
    string AssetSymbol,
    List<string> IndicatorsUsed,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<ResearchStudy>;

public class CreateResearchStudyCommandHandler : IRequestHandler<CreateResearchStudyCommand, ResearchStudy>
{
    // TODO: Implementar repositório posteriormente
    public async Task<ResearchStudy> Handle(CreateResearchStudyCommand request, CancellationToken cancellationToken)
    {
        var study = new ResearchStudy
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            AssetSymbol = request.AssetSymbol,
            IndicatorsUsed = request.IndicatorsUsed,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedAt = DateTime.UtcNow
        };

        return await Task.FromResult(study);
    }
}
