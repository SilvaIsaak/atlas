using MediatR;
using CryptoAIPlatform.Domain.Learning;

namespace CryptoAIPlatform.Application.Learning;

public record GetLearningContentQuery : IRequest<GetLearningContentResponse>
{
    public Guid ContentId { get; init; }
}
