using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.MarketScanner;
using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Presentation.Controllers;

/// <summary>
/// Controlador de dados de mercado. Obtém tickers, livros de ordens e candles (klines).
/// </summary>
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

    /// <summary>
    /// Obtém o ticker (preço atual) de um par em uma exchange.
    /// </summary>
    /// <param name="exchangeCode">Código da exchange (ex: binance).</param>
    /// <param name="symbol">Par de negociação (ex: BTCUSDT).</param>
    /// <returns>Dados do ticker.</returns>
    [HttpGet("ticker")]
    [ProducesResponseType(typeof(ExchangeTicker), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTicker([FromQuery] string exchangeCode, [FromQuery] string symbol)
    {
        var result = await _mediator.Send(new GetMarketTickerQuery(exchangeCode, symbol));
        return Ok(result);
    }

    /// <summary>
    /// Obtém o livro de ordens de um par em uma exchange.
    /// </summary>
    /// <param name="exchangeCode">Código da exchange (ex: binance).</param>
    /// <param name="symbol">Par de negociação (ex: BTCUSDT).</param>
    /// <param name="limit">Número de ordens a retornar (padrão: 20).</param>
    /// <returns>Livro de ordens.</returns>
    [HttpGet("orderbook")]
    [ProducesResponseType(typeof(ExchangeOrderBook), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderBook([FromQuery] string exchangeCode, [FromQuery] string symbol, [FromQuery] int limit = 20)
    {
        var result = await _mediator.Send(new GetMarketOrderBookQuery(exchangeCode, symbol, limit));
        return Ok(result);
    }

    /// <summary>
    /// Obtém candles (klines) históricos de um par em uma exchange.
    /// </summary>
    /// <param name="exchangeCode">Código da exchange (ex: binance).</param>
    /// <param name="symbol">Par de negociação (ex: BTCUSDT).</param>
    /// <param name="interval">Intervalo dos candles (ex: 1m, 1h, 1d).</param>
    /// <param name="startTime">Data de início (opcional).</param>
    /// <param name="endTime">Data de fim (opcional).</param>
    /// <returns>Lista de candles.</returns>
    [HttpGet("klines")]
    [ProducesResponseType(typeof(List<ExchangeKline>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetKlines([FromQuery] string exchangeCode, [FromQuery] string symbol, [FromQuery] string interval, [FromQuery] DateTime? startTime = null, [FromQuery] DateTime? endTime = null)
    {
        var result = await _mediator.Send(new GetMarketKlinesQuery(exchangeCode, symbol, interval, startTime, endTime));
        return Ok(result);
    }
}
