using MediatR;

namespace CryptoAIPlatform.Application.Execution;

public record CreateExecutionEngineCommand : IRequest<CreateExecutionEngineResponse>
{
    public Guid UserId { get; init; }
    public Guid ExchangeIntegrationId { get; init; }
}

public record CreateExecutionEngineResponse
{
    public Guid ExecutionEngineId { get; init; }
    public bool IsActive { get; init; }
}