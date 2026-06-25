using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Execution;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Execution;

public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, SubmitOrderResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public SubmitOrderCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SubmitOrderResponse> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var executionEngine = await _dbContext.ExecutionEngines
            .FirstOrDefaultAsync(ee => ee.Id == request.ExecutionEngineId && ee.UserId == request.UserId, cancellationToken);

        if (executionEngine == null)
        {
            throw new KeyNotFoundException("ExecutionEngine not found");
        }

        var order = new ExecutionOrder(
            OrderId: Guid.NewGuid(),
            ExchangeOrderId: Guid.NewGuid().ToString(), // Simulação de ID da exchange
            Symbol: request.Symbol,
            OrderType: request.OrderType,
            Side: request.Side,
            Quantity: request.Quantity,
            Price: request.Price,
            StopPrice: request.StopPrice,
            ExecutedQuantity: null,
            AveragePrice: null,
            Status: ExecutionStatus.Pending,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: null,
            ExecutedAt: null
        );

        executionEngine.Orders ??= new List<ExecutionOrder>();
        executionEngine.Orders.Add(order);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SubmitOrderResponse
        {
            OrderId = order.OrderId,
            Status = order.Status
        };
    }
}