using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.Backtesting;
using CryptoAIPlatform.Domain.Backtesting;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/backtesting")]
[ApiVersion("1.0")]
[Authorize]
public class BacktestingController : ControllerBase
{
    private readonly IMediator _mediator;

    public BacktestingController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetBacktestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBacktests()
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllBacktestsQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{backtestId:guid}")]
    [ProducesResponseType(typeof(GetBacktestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBacktest(Guid backtestId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetBacktestQuery { BacktestId = backtestId, UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateBacktestResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBacktest([FromBody] CreateBacktestRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new CreateBacktestCommand
        {
            UserId = userId,
            StrategyId = request.StrategyId,
            Name = request.Name,
            Description = request.Description,
            AssetSymbol = request.AssetSymbol,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            InitialCapital = request.InitialCapital
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBacktest), new { backtestId = result.BacktestId }, result);
    }

    [HttpPost("{backtestId:guid}/execute")]
    [ProducesResponseType(typeof(ExecuteBacktestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExecuteBacktest(Guid backtestId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new ExecuteBacktestCommand { BacktestId = backtestId, UserId = userId });
        return Ok(result);
    }
}

public record CreateBacktestRequest
{
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal InitialCapital { get; init; }
}