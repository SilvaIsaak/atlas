using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.Reports;
using CryptoAIPlatform.Domain.Reports;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/reports")]
[ApiVersion("1.0")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetReportResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReports([FromQuery] ReportType? type)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetReportsQuery
        {
            UserId = userId,
            Type = type
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateReportResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportRequest request)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new CreateReportCommand
        {
            UserId = userId,
            Type = request.Type,
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        });
        return CreatedAtAction(nameof(GetReports), result);
    }
}

public record CreateReportRequest
{
    public ReportType Type { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
