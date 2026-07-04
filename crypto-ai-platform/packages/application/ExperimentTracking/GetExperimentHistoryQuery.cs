using MediatR;

namespace CryptoAIPlatform.Application.ExperimentTracking;

public record GetExperimentHistoryQuery(Guid OwnerId) : IRequest<IReadOnlyList<ExperimentDto>>;
