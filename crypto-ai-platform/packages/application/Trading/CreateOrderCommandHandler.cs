using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Trading;
using CryptoAIPlatform.Domain.Trading.Enums;
using CryptoAIPlatform.Domain.Trading.Repositories;
using CryptoAIPlatform.Domain.Trading.ValueObjects;

namespace CryptoAIPlatform.Application.Trading;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating order for portfolio {PortfolioId}", request.PortfolioId);

        var order = Order.Create(
            id: Guid.NewGuid(),
            tenantId: TenantId.Default,
            portfolioId: new PortfolioId(request.PortfolioId),
            symbol: request.Symbol,
            side: Enum.Parse<OrderSide>(request.Side),
            type: Enum.Parse<OrderType>(request.Type),
            timeInForce: Enum.Parse<TimeInForce>(request.TimeInForce),
            quantity: new OrderQuantity(request.Quantity),
            price: request.Price.HasValue ? new OrderPrice(request.Price.Value) : null,
            stopLoss: request.StopLoss.HasValue ? new StopLoss(request.StopLoss.Value) : null,
            takeProfit: request.TakeProfit.HasValue ? new TakeProfit(request.TakeProfit.Value) : null,
            leverage: request.Leverage.HasValue ? new Leverage(request.Leverage.Value) : null);

        await _orderRepository.AddAsync(order, cancellationToken);

        return new OrderDto(
            Id: order.Id,
            PortfolioId: order.PortfolioId.Value,
            Symbol: order.Symbol,
            Side: order.Side.ToString(),
            Type: order.Type.ToString(),
            TimeInForce: order.TimeInForce.ToString(),
            Quantity: order.Quantity.Value,
            Price: order.Price?.Value,
            Status: order.Status.ToString(),
            CreatedAt: order.CreatedAt);
    }
}

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CancelOrderCommandHandler> _logger;

    public CancelOrderCommandHandler(
        IOrderRepository orderRepository,
        ILogger<CancelOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<OrderDto> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cancelling order {OrderId}", request.OrderId);
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            throw new InvalidOperationException("Order not found");

        order.Cancel(request.Reason);
        await _orderRepository.UpdateAsync(order, cancellationToken);

        return new OrderDto(
            Id: order.Id,
            PortfolioId: order.PortfolioId.Value,
            Symbol: order.Symbol,
            Side: order.Side.ToString(),
            Type: order.Type.ToString(),
            TimeInForce: order.TimeInForce.ToString(),
            Quantity: order.Quantity.Value,
            Price: order.Price?.Value,
            Status: order.Status.ToString(),
            CreatedAt: order.CreatedAt);
    }
}

public class ExecuteOrderCommandHandler : IRequestHandler<ExecuteOrderCommand, TradeExecutionDto>
{
    private readonly ITradeExecutionRepository _executionRepository;
    private readonly ILogger<ExecuteOrderCommandHandler> _logger;

    public ExecuteOrderCommandHandler(
        ITradeExecutionRepository executionRepository,
        ILogger<ExecuteOrderCommandHandler> logger)
    {
        _executionRepository = executionRepository;
        _logger = logger;
    }

    public async Task<TradeExecutionDto> Handle(ExecuteOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting execution for order {OrderId}", request.OrderId);
        var execution = TradeExecution.Create(
            Guid.NewGuid(),
            TenantId.Default,
            request.OrderId);

        await _executionRepository.AddAsync(execution, cancellationToken);

        return new TradeExecutionDto(
            execution.Id,
            execution.OrderId,
            execution.Status.ToString(),
            execution.StartedAt);
    }
}

public class OpenPositionCommandHandler : IRequestHandler<OpenPositionCommand, PositionDto>
{
    private readonly IPositionRepository _positionRepository;
    private readonly ILogger<OpenPositionCommandHandler> _logger;

    public OpenPositionCommandHandler(
        IPositionRepository positionRepository,
        ILogger<OpenPositionCommandHandler> logger)
    {
        _positionRepository = positionRepository;
        _logger = logger;
    }

    public async Task<PositionDto> Handle(OpenPositionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Opening position for portfolio {PortfolioId}", request.PortfolioId);
        var position = Position.Create(
            Guid.NewGuid(),
            TenantId.Default,
            new PortfolioId(request.PortfolioId),
            request.Symbol,
            new OrderQuantity(request.Quantity),
            new OrderPrice(request.EntryPrice),
            new Leverage(request.Leverage),
            new Margin(0, 0),
            null,
            null);

        await _positionRepository.AddAsync(position, cancellationToken);

        return new PositionDto(
            position.Id,
            position.PortfolioId.Value,
            position.Symbol,
            position.Quantity.Value,
            position.EntryPrice.Value,
            position.Status.ToString(),
            position.OpenedAt);
    }
}

public class ClosePositionCommandHandler : IRequestHandler<ClosePositionCommand, PositionDto>
{
    private readonly IPositionRepository _positionRepository;
    private readonly ILogger<ClosePositionCommandHandler> _logger;

    public ClosePositionCommandHandler(
        IPositionRepository positionRepository,
        ILogger<ClosePositionCommandHandler> logger)
    {
        _positionRepository = positionRepository;
        _logger = logger;
    }

    public async Task<PositionDto> Handle(ClosePositionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Closing position {PositionId}", request.PositionId);
        var position = await _positionRepository.GetByIdAsync(request.PositionId, cancellationToken);
        if (position is null)
            throw new InvalidOperationException("Position not found");

        position.Close();
        await _positionRepository.UpdateAsync(position, cancellationToken);

        return new PositionDto(
            position.Id,
            position.PortfolioId.Value,
            position.Symbol,
            position.Quantity.Value,
            position.EntryPrice.Value,
            position.Status.ToString(),
            position.OpenedAt);
    }
}

public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand, PortfolioDto>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly ILogger<UpdatePortfolioCommandHandler> _logger;

    public UpdatePortfolioCommandHandler(
        IPortfolioRepository portfolioRepository,
        ILogger<UpdatePortfolioCommandHandler> logger)
    {
        _portfolioRepository = portfolioRepository;
        _logger = logger;
    }

    public async Task<PortfolioDto> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating portfolio {PortfolioId}", request.PortfolioId);
        var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId, cancellationToken);
        if (portfolio is null)
            throw new InvalidOperationException("Portfolio not found");

        await _portfolioRepository.UpdateAsync(portfolio, cancellationToken);
        return new PortfolioDto(
            portfolio.Id,
            portfolio.Name,
            portfolio.Status.ToString(),
            portfolio.CreatedAt);
    }
}
