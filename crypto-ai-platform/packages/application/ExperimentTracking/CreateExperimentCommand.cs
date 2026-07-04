using MediatR;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Application.ExperimentTracking;

public record CreateExperimentCommand(
    string Name,
    string Description,
    ExperimentType Type,
    Guid OwnerId,
    Dictionary<string, string> Parameters) : IRequest<ExperimentDto>;
