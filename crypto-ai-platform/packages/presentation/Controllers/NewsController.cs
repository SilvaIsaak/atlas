using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.News;
using CryptoAIPlatform.Domain.News;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/news")]
[ApiVersion("1.0")]
[Authorize]
public class NewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<News>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNews([FromQuery] string? assetSymbol = null, [FromQuery] int limit = 50)
    {
        var result = await _mediator.Send(new GetNewsQuery(assetSymbol, limit));
        return Ok(result);
    }

    [HttpPost("analyze")]
    [ProducesResponseType(typeof(NewsAnalysis), StatusCodes.Status200OK)]
    public async Task<IActionResult> AnalyzeSentiment([FromBody] News news)
    {
        var result = await _mediator.Send(new AnalyzeNewsSentimentCommand(news));
        return Ok(result);
    }
}
