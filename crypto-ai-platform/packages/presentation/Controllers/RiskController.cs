using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.RiskManagement;
using CryptoAIPlatform.Domain.RiskManagement;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/risk")]
[ApiVersion("1.0")]
[Authorize]
public class RiskController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetRiskProfileResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRiskProfiles()
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllRiskProfilesQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{riskProfileId:guid}")]
    [ProducesResponseType(typeof(GetRiskProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRiskProfile(Guid riskProfileId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetRiskProfileQuery { RiskProfileId = riskProfileId, UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateRiskProfileResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateRiskProfile([FromBody] CreateRiskProfileRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new CreateRiskProfileCommand
        {
            UserId = userId,
            Name = request.Name,
            Rules = request.Rules
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRiskProfile), new { riskProfileId = result.RiskProfileId }, result);
    }
}

public record CreateRiskProfileRequest
{
    public string Name { get; init; } = string.Empty;
    public List<RiskRule>? Rules { get; init; }
}