using MediatR;
using CryptoAIPlatform.Domain.Execution;

namespace CryptoAIPlatform.Application.Execution;

public record SubmitOrderCommand : IRequest<SubmitOrderResponse>
{
    public Guid UserId { get; init; }
    public Guid ExecutionEngineId { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string OrderType { get; init; } = string.Empty;
    public string Side { get; init; } = string.Empty;
    public decimal Quantity { get; init; }
    public decimal? Price { get; init; }
    public decimal? StopPrice { get; init; }
}

public record SubmitOrderResponse
{
    public Guid OrderId { get; init; }
    public ExecutionStatus Status { get; init; }
}