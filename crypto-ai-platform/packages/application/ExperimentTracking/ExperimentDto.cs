using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Application.ExperimentTracking;

public record ExperimentDto(
    Guid Id,
    string Name,
    string Description,
    ExperimentType Type,
    Guid OwnerId,
    DateTime CreatedAt);
