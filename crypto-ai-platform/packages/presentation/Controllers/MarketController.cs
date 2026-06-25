using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.MarketScanner;
using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/market")]
[ApiVersion("1.0")]
[Authorize]
public class MarketController : ControllerBase
{
    private readonly IMediator _mediator;

    public MarketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ticker")]
    [ProducesResponseType(typeof(ExchangeTicker), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTicker([FromQuery] string exchangeCode, [FromQuery] string symbol)
    {
        var result = await _mediator.Send(new GetMarketTickerQuery(exchangeCode, symbol));
        return Ok(result);
    }

    [HttpGet("orderbook")]
    [ProducesResponseType(typeof(ExchangeOrderBook), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderBook([FromQuery] string exchangeCode, [FromQuery] string symbol, [FromQuery] int limit = 20)
    {
        var result = await _mediator.Send(new GetMarketOrderBookQuery(exchangeCode, symbol, limit));
        return Ok(result);
    }

    [HttpGet("klines")]
    [ProducesResponseType(typeof(List<ExchangeKline>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetKlines([FromQuery] string exchangeCode, [FromQuery] string symbol, [FromQuery] string interval, [FromQuery] DateTime? startTime = null, [FromQuery] DateTime? endTime = null)
    {
        var result = await _mediator.Send(new GetMarketKlinesQuery(exchangeCode, symbol, interval, startTime, endTime));
        return Ok(result);
    }
}
