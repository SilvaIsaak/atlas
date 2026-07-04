using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Trading.Services;

namespace CryptoAIPlatform.Infrastructure.Trading;

public class TradingEngineService : ITradingEngine
{
    private readonly ILogger<TradingEngineService> _logger;

    public TradingEngineService(ILogger<TradingEngineService> logger)
    {
        _logger = logger;
    }
}

public class RiskEngineService : IRiskEngine
{
    private readonly ILogger<RiskEngineService> _logger;

    public RiskEngineService(ILogger<RiskEngineService> logger)
    {
        _logger = logger;
    }
}

public class OrderExecutionService : IOrderExecutionService
{
    private readonly ILogger<OrderExecutionService> _logger;

    public OrderExecutionService(ILogger<OrderExecutionService> logger)
    {
        _logger = logger;
    }
}

public class PortfolioService : IPortfolioService
{
    private readonly ILogger<PortfolioService> _logger;

    public PortfolioService(ILogger<PortfolioService> logger)
    {
        _logger = logger;
    }
}

public class PositionService : IPositionService
{
    private readonly ILogger<PositionService> _logger;

    public PositionService(ILogger<PositionService> logger)
    {
        _logger = logger;
    }
}
