using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.LiveTrading;
using CryptoAIPlatform.Domain.LiveTrading;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/live-trading")]
[ApiVersion("1.0")]
[Authorize]
public class LiveTradingController : ControllerBase
{
    private readonly IMediator _mediator;

    public LiveTradingController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetLiveTradeResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLiveTrades()
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllLiveTradesQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{liveTradeId:guid}")]
    [ProducesResponseType(typeof(GetLiveTradeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLiveTrade(Guid liveTradeId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetLiveTradeQuery { LiveTradeId = liveTradeId, UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateLiveTradeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateLiveTrade([FromBody] CreateLiveTradeRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new CreateLiveTradeCommand
        {
            UserId = userId,
            StrategyId = request.StrategyId,
            ExecutionEngineId = request.ExecutionEngineId,
            Name = request.Name,
            AssetSymbol = request.AssetSymbol,
            InitialCapital = request.InitialCapital
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetLiveTrade), new { liveTradeId = result.LiveTradeId }, result);
    }

    [HttpPost("{liveTradeId:guid}/start")]
    [ProducesResponseType(typeof(StartLiveTradeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> StartLiveTrade(Guid liveTradeId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new StartLiveTradeCommand { LiveTradeId = liveTradeId, UserId = userId });
        return Ok(result);
    }
}

public record CreateLiveTradeRequest
{
    public Guid StrategyId { get; init; }
    public Guid ExecutionEngineId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public decimal InitialCapital { get; init; }
}