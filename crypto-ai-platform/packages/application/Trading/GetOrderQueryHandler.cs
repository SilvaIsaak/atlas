using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Trading.Repositories;

namespace CryptoAIPlatform.Application.Trading;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GetOrderQueryHandler> _logger;

    public GetOrderQueryHandler(
        IOrderRepository orderRepository,
        ILogger<GetOrderQueryHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<OrderDto?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting order {OrderId}", request.OrderId);
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null) return null;
        return new OrderDto(
            order.Id,
            order.PortfolioId.Value,
            order.Symbol,
            order.Side.ToString(),
            order.Type.ToString(),
            order.TimeInForce.ToString(),
            order.Quantity.Value,
            order.Price?.Value,
            order.Status.ToString(),
            order.CreatedAt);
    }
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GetOrdersQueryHandler> _logger;

    public GetOrdersQueryHandler(
        IOrderRepository orderRepository,
        ILogger<GetOrdersQueryHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting orders for portfolio {PortfolioId}", request.PortfolioId);
        var orders = await _orderRepository.GetByPortfolioIdAsync(request.PortfolioId, cancellationToken);
        return orders.Select(order => new OrderDto(
            order.Id,
            order.PortfolioId.Value,
            order.Symbol,
            order.Side.ToString(),
            order.Type.ToString(),
            order.TimeInForce.ToString(),
            order.Quantity.Value,
            order.Price?.Value,
            order.Status.ToString(),
            order.CreatedAt)).ToList();
    }
}

public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionDto?>
{
    private readonly IPositionRepository _positionRepository;
    private readonly ILogger<GetPositionQueryHandler> _logger;

    public GetPositionQueryHandler(
        IPositionRepository positionRepository,
        ILogger<GetPositionQueryHandler> logger)
    {
        _positionRepository = positionRepository;
        _logger = logger;
    }

    public async Task<PositionDto?> Handle(GetPositionQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting position {PositionId}", request.PositionId);
        var position = await _positionRepository.GetByIdAsync(request.PositionId, cancellationToken);
        if (position is null) return null;
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

public class GetPortfolioQueryHandler : IRequestHandler<GetPortfolioQuery, PortfolioDto?>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly ILogger<GetPortfolioQueryHandler> _logger;

    public GetPortfolioQueryHandler(
        IPortfolioRepository portfolioRepository,
        ILogger<GetPortfolioQueryHandler> logger)
    {
        _portfolioRepository = portfolioRepository;
        _logger = logger;
    }

    public async Task<PortfolioDto?> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting portfolio {PortfolioId}", request.PortfolioId);
        var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId, cancellationToken);
        if (portfolio is null) return null;
        return new PortfolioDto(
            portfolio.Id,
            portfolio.Name,
            portfolio.Status.ToString(),
            portfolio.CreatedAt);
    }
}

public class GetExecutionsQueryHandler : IRequestHandler<GetExecutionsQuery, IEnumerable<TradeExecutionDto>>
{
    private readonly ITradeExecutionRepository _executionRepository;
    private readonly ILogger<GetExecutionsQueryHandler> _logger;

    public GetExecutionsQueryHandler(
        ITradeExecutionRepository executionRepository,
        ILogger<GetExecutionsQueryHandler> logger)
    {
        _executionRepository = executionRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<TradeExecutionDto>> Handle(GetExecutionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting executions for order {OrderId}", request.OrderId);
        var executions = await _executionRepository.GetByOrderIdAsync(request.OrderId, cancellationToken);
        return executions.Select(e => new TradeExecutionDto(
            e.Id,
            e.OrderId,
            e.Status.ToString(),
            e.StartedAt)).ToList();
    }
}
