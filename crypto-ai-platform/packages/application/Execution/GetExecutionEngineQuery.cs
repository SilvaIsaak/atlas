using MediatR;
using CryptoAIPlatform.Domain.Execution;

namespace CryptoAIPlatform.Application.Execution;

public record GetExecutionEngineQuery : IRequest<GetExecutionEngineResponse>
{
    public Guid ExecutionEngineId { get; init; }
    public Guid UserId { get; init; }
}

public record GetExecutionEngineResponse(
    Guid ExecutionEngineId,
    Guid UserId,
    Guid ExchangeIntegrationId,
    List<ExecutionOrder>? Orders,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);