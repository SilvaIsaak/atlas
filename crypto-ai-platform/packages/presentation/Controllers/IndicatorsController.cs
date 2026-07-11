using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.Indicators;
using CryptoAIPlatform.Domain.Indicators;

namespace CryptoAIPlatform.Presentation.Controllers;

/// <summary>
/// Controlador de indicadores técnicos. Calcula SMA, EMA, RSI, MACD e Bandas de Bollinger.
/// </summary>
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

    /// <summary>
    /// Calcula a Média Móvel Simples (SMA).
    /// </summary>
    /// <param name="query">Parâmetros para cálculo (dados, período).</param>
    /// <returns>Resultado do SMA.</returns>
    [HttpPost("sma")]
    [ProducesResponseType(typeof(SmaResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateSma([FromBody] CalculateSmaQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Calcula a Média Móvel Exponencial (EMA).
    /// </summary>
    /// <param name="query">Parâmetros para cálculo (dados, período).</param>
    /// <returns>Resultado do EMA.</returns>
    [HttpPost("ema")]
    [ProducesResponseType(typeof(EmaResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateEma([FromBody] CalculateEmaQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Calcula o Índice de Força Relativa (RSI).
    /// </summary>
    /// <param name="query">Parâmetros para cálculo (dados, período).</param>
    /// <returns>Resultado do RSI.</returns>
    [HttpPost("rsi")]
    [ProducesResponseType(typeof(RsiResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateRsi([FromBody] CalculateRsiQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Calcula o Moving Average Convergence Divergence (MACD).
    /// </summary>
    /// <param name="query">Parâmetros para cálculo (dados, períodos).</param>
    /// <returns>Resultado do MACD.</returns>
    [HttpPost("macd")]
    [ProducesResponseType(typeof(MacdResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateMacd([FromBody] CalculateMacdQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Calcula as Bandas de Bollinger.
    /// </summary>
    /// <param name="query">Parâmetros para cálculo (dados, período, desvio padrão).</param>
    /// <returns>Resultado das Bandas de Bollinger.</returns>
    [HttpPost("bollinger-bands")]
    [ProducesResponseType(typeof(BollingerBandsResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateBollingerBands([FromBody] CalculateBollingerBandsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
