using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.Deployment;
using CryptoAIPlatform.Domain.Deployment;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/deployment")]
[ApiVersion("1.0")]
[Authorize]
public class DeploymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeploymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetDeploymentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDeployments(
        [FromQuery] DeploymentStatus? status,
        [FromQuery] string? environment,
        [FromQuery] int? limit = 50)
    {
        var result = await _mediator.Send(new GetDeploymentsQuery
        {
            Status = status,
            Environment = environment,
            Limit = limit
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateDeploymentResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDeployment([FromBody] CreateDeploymentRequest request)
    {
        var result = await _mediator.Send(new CreateDeploymentCommand
        {
            Version = request.Version,
            Description = request.Description,
            BuildNumber = request.BuildNumber,
            Environment = request.Environment
        });
        return CreatedAtAction(nameof(GetDeployments), result);
    }
}

public record CreateDeploymentRequest
{
    public string Version { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? BuildNumber { get; init; }
    public string? Environment { get; init; }
}
