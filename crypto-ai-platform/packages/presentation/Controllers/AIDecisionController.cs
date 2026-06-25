using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.AIDecision;
using CryptoAIPlatform.Domain.AIDecision;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/ai-decision")]
[ApiVersion("1.0")]
[Authorize]
public class AIDecisionController : ControllerBase
{
    private readonly IMediator _mediator;

    public AIDecisionController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetAIDecisionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAIDecisions([FromQuery] Guid? strategyId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllAIDecisionsQuery { UserId = userId, StrategyId = strategyId });
        return Ok(result);
    }

    [HttpGet("{aiDecisionId:guid}")]
    [ProducesResponseType(typeof(GetAIDecisionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAIDecision(Guid aiDecisionId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAIDecisionQuery { AIDecisionId = aiDecisionId, UserId = userId });
        return Ok(result);
    }

    [HttpPost("generate")]
    [ProducesResponseType(typeof(GenerateAIDecisionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateAIDecision([FromBody] GenerateAIDecisionRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new GenerateAIDecisionCommand
        {
            UserId = userId,
            StrategyId = request.StrategyId,
            Symbol = request.Symbol,
            ModelConfig = request.ModelConfig
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

public record GenerateAIDecisionRequest
{
    public Guid StrategyId { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public AIModelConfig? ModelConfig { get; init; }
}