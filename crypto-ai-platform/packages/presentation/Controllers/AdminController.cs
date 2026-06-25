using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.Admin;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/admin")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("logs")]
    [ProducesResponseType(typeof(List<GetAdminLogResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdminLogs([FromQuery] int limit = 100)
    {
        var result = await _mediator.Send(new GetAdminLogsQuery
        {
            Limit = limit
        });
        return Ok(result);
    }
}
