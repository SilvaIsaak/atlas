using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.Strategies;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/strategies")]
[ApiVersion("1.0")]
[Authorize]
public class StrategiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public StrategiesController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetStrategyResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStrategies()
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllStrategiesQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{strategyId:guid}")]
    [ProducesResponseType(typeof(GetStrategyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStrategy(Guid strategyId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetStrategyQuery { StrategyId = strategyId, UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateStrategyResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateStrategy([FromBody] CreateStrategyRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new CreateStrategyCommand
        {
            UserId = userId,
            Name = request.Name,
            Description = request.Description,
            Code = request.Code,
            AssetSymbol = request.AssetSymbol,
            ResearchStudyId = request.ResearchStudyId
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStrategy), new { strategyId = result.StrategyId }, result);
    }

    [HttpPut("{strategyId:guid}")]
    [ProducesResponseType(typeof(UpdateStrategyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStrategy(Guid strategyId, [FromBody] UpdateStrategyRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new UpdateStrategyCommand
        {
            StrategyId = strategyId,
            UserId = userId,
            Name = request.Name,
            Description = request.Description,
            Code = request.Code,
            AssetSymbol = request.AssetSymbol
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch("{strategyId:guid}/status")]
    [ProducesResponseType(typeof(ChangeStrategyStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeStrategyStatus(Guid strategyId, [FromBody] ChangeStrategyStatusRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new ChangeStrategyStatusCommand
        {
            StrategyId = strategyId,
            UserId = userId,
            NewStatus = request.NewStatus
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

public record CreateStrategyRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public Guid? ResearchStudyId { get; init; }
}

public record UpdateStrategyRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
}

public record ChangeStrategyStatusRequest
{
    public StrategyStatus NewStatus { get; init; }
}
