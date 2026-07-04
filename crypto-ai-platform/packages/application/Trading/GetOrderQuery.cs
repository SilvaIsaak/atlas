using MediatR;

namespace CryptoAIPlatform.Application.Trading;

public record GetOrderQuery(Guid OrderId) : IRequest<OrderDto?>;
public record GetOrdersQuery(Guid PortfolioId) : IRequest<IEnumerable<OrderDto>>;
public record GetPositionQuery(Guid PositionId) : IRequest<PositionDto?>;
public record GetPortfolioQuery(Guid PortfolioId) : IRequest<PortfolioDto?>;
public record GetExecutionsQuery(Guid OrderId) : IRequest<IEnumerable<TradeExecutionDto>>;
