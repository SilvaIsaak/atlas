using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.Monitoring;
using CryptoAIPlatform.Domain.Monitoring;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/monitoring")]
[ApiVersion("1.0")]
[Authorize]
public class MonitoringController : ControllerBase
{
    private readonly IMediator _mediator;

    public MonitoringController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("metrics")]
    [ProducesResponseType(typeof(List<GetSystemMetricResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSystemMetrics(
        [FromQuery] MetricType? type,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int? limit = 100)
    {
        var result = await _mediator.Send(new GetSystemMetricsQuery
        {
            Type = type,
            StartDate = startDate,
            EndDate = endDate,
            Limit = limit
        });
        return Ok(result);
    }

    [HttpGet("alerts")]
    [ProducesResponseType(typeof(List<GetSystemAlertResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSystemAlerts(
        [FromQuery] bool? onlyUnacknowledged,
        [FromQuery] string? severity,
        [FromQuery] int? limit = 50)
    {
        var result = await _mediator.Send(new GetSystemAlertsQuery
        {
            OnlyUnacknowledged = onlyUnacknowledged,
            Severity = severity,
            Limit = limit
        });
        return Ok(result);
    }

    [HttpPatch("alerts/{alertId:guid}/acknowledge")]
    [ProducesResponseType(typeof(AcknowledgeSystemAlertResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AcknowledgeAlert(Guid alertId)
    {
        var result = await _mediator.Send(new AcknowledgeSystemAlertCommand { AlertId = alertId });
        return Ok(result);
    }
}
