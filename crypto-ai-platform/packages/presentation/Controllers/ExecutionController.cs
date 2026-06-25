using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.Execution;
using CryptoAIPlatform.Domain.Execution;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/execution")]
[ApiVersion("1.0")]
[Authorize]
public class ExecutionController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExecutionController(IMediator mediator)
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

    [HttpGet("{executionEngineId:guid}")]
    [ProducesResponseType(typeof(GetExecutionEngineResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExecutionEngine(Guid executionEngineId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetExecutionEngineQuery { ExecutionEngineId = executionEngineId, UserId = userId });
        return Ok(result);
    }

    [HttpPost("engine")]
    [ProducesResponseType(typeof(CreateExecutionEngineResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateExecutionEngine([FromBody] CreateExecutionEngineRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new CreateExecutionEngineCommand
        {
            UserId = userId,
            ExchangeIntegrationId = request.ExchangeIntegrationId
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetExecutionEngine), new { executionEngineId = result.ExecutionEngineId }, result);
    }

    [HttpPost("{executionEngineId:guid}/orders")]
    [ProducesResponseType(typeof(SubmitOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmitOrder(Guid executionEngineId, [FromBody] SubmitOrderRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new SubmitOrderCommand
        {
            UserId = userId,
            ExecutionEngineId = executionEngineId,
            Symbol = request.Symbol,
            OrderType = request.OrderType,
            Side = request.Side,
            Quantity = request.Quantity,
            Price = request.Price,
            StopPrice = request.StopPrice
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

public record CreateExecutionEngineRequest
{
    public Guid ExchangeIntegrationId { get; init; }
}

public record SubmitOrderRequest
{
    public string Symbol { get; init; } = string.Empty;
    public string OrderType { get; init; } = string.Empty;
    public string Side { get; init; } = string.Empty;
    public decimal Quantity { get; init; }
    public decimal? Price { get; init; }
    public decimal? StopPrice { get; init; }
}