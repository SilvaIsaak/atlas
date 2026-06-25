using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.PaperTrading;
using CryptoAIPlatform.Domain.PaperTrading;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/papertrading")]
[ApiVersion("1.0")]
[Authorize]
public class PaperTradingController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaperTradingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }
        return Guid.Parse(userIdClaim.Value);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetPaperTradeResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPaperTrades()
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllPaperTradesQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{paperTradeId:guid}")]
    [ProducesResponseType(typeof(GetPaperTradeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaperTrade(Guid paperTradeId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetPaperTradeQuery { PaperTradeId = paperTradeId, UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePaperTradeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePaperTrade([FromBody] CreatePaperTradeRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new CreatePaperTradeCommand
        {
            UserId = userId,
            StrategyId = request.StrategyId,
            Name = request.Name,
            Description = request.Description,
            AssetSymbol = request.AssetSymbol,
            InitialCapital = request.InitialCapital
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPaperTrade), new { paperTradeId = result.PaperTradeId }, result);
    }

    [HttpPost("{paperTradeId:guid}/start")]
    [ProducesResponseType(typeof(StartPaperTradeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> StartPaperTrade(Guid paperTradeId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new StartPaperTradeCommand { PaperTradeId = paperTradeId, UserId = userId });
        return Ok(result);
    }
}

public record CreatePaperTradeRequest
{
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public decimal InitialCapital { get; init; }
}