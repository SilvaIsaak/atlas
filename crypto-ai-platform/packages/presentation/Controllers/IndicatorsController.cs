using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.Indicators;
using CryptoAIPlatform.Domain.Indicators;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/indicators")]
[ApiVersion("1.0")]
[Authorize]
public class IndicatorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndicatorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sma")]
    [ProducesResponseType(typeof(SmaResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateSma([FromBody] CalculateSmaQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("ema")]
    [ProducesResponseType(typeof(EmaResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateEma([FromBody] CalculateEmaQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("rsi")]
    [ProducesResponseType(typeof(RsiResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateRsi([FromBody] CalculateRsiQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("macd")]
    [ProducesResponseType(typeof(MacdResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateMacd([FromBody] CalculateMacdQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("bollinger-bands")]
    [ProducesResponseType(typeof(BollingerBandsResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateBollingerBands([FromBody] CalculateBollingerBandsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
