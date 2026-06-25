using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.Learning;
using CryptoAIPlatform.Domain.Learning;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/learning")]
[ApiVersion("1.0")]
public class LearningController : ControllerBase
{
    private readonly IMediator _mediator;

    public LearningController(IMediator mediator)
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

    [HttpGet("contents")]
    [ProducesResponseType(typeof(List<GetLearningContentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLearningContents(
        [FromQuery] ContentType? type,
        [FromQuery] bool? onlyPublished = true,
        [FromQuery] string? searchTerm = null)
    {
        var result = await _mediator.Send(new GetLearningContentsQuery
        {
            Type = type,
            OnlyPublished = onlyPublished,
            SearchTerm = searchTerm
        });
        return Ok(result);
    }

    [HttpGet("contents/{contentId:guid}")]
    [ProducesResponseType(typeof(GetLearningContentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLearningContent(Guid contentId)
    {
        var result = await _mediator.Send(new GetLearningContentQuery { ContentId = contentId });
        return Ok(result);
    }

    [HttpGet("progress")]
    [Authorize]
    [ProducesResponseType(typeof(List<GetUserProgressResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProgress()
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetUserProgressQuery { UserId = userId });
        return Ok(result);
    }

    [HttpPatch("progress")]
    [Authorize]
    [ProducesResponseType(typeof(UpdateUserProgressResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUserProgress([FromBody] UpdateUserProgressRequest request)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new UpdateUserProgressCommand
        {
            UserId = userId,
            ContentId = request.ContentId,
            ProgressPercentage = request.ProgressPercentage,
            IsCompleted = request.IsCompleted
        });
        return Ok(result);
    }
}

public record UpdateUserProgressRequest
{
    public Guid ContentId { get; init; }
    public int ProgressPercentage { get; init; }
    public bool IsCompleted { get; init; }
}
