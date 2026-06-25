using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.WalkForward;
using CryptoAIPlatform.Domain.WalkForward;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/walkforward")]
[ApiVersion("1.0")]
[Authorize]
public class WalkForwardController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalkForwardController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetWalkForwardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWalkForwards()
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllWalkForwardsQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{walkForwardId:guid}")]
    [ProducesResponseType(typeof(GetWalkForwardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWalkForward(Guid walkForwardId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetWalkForwardQuery { WalkForwardId = walkForwardId, UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateWalkForwardResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateWalkForward([FromBody] CreateWalkForwardRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new CreateWalkForwardCommand
        {
            UserId = userId,
            StrategyId = request.StrategyId,
            Name = request.Name,
            Description = request.Description,
            AssetSymbol = request.AssetSymbol,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TrainingWindowDays = request.TrainingWindowDays,
            TestingWindowDays = request.TestingWindowDays,
            InitialCapital = request.InitialCapital
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetWalkForward), new { walkForwardId = result.WalkForwardId }, result);
    }

    [HttpPost("{walkForwardId:guid}/execute")]
    [ProducesResponseType(typeof(ExecuteWalkForwardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExecuteWalkForward(Guid walkForwardId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new ExecuteWalkForwardCommand { WalkForwardId = walkForwardId, UserId = userId });
        return Ok(result);
    }
}

public record CreateWalkForwardRequest
{
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int TrainingWindowDays { get; init; }
    public int TestingWindowDays { get; init; }
    public decimal InitialCapital { get; init; }
}