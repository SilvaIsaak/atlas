using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.Mobile;
using CryptoAIPlatform.Domain.Mobile;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/mobile")]
[ApiVersion("1.0")]
[Authorize]
public class MobileController : ControllerBase
{
    private readonly IMediator _mediator;

    public MobileController(IMediator mediator)
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

    [HttpPost("devices")]
    [ProducesResponseType(typeof(RegisterMobileDeviceResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterDevice([FromBody] RegisterMobileDeviceRequest request)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new RegisterMobileDeviceCommand
        {
            UserId = userId,
            Platform = request.Platform,
            DeviceToken = request.DeviceToken,
            DeviceModel = request.DeviceModel,
            OsVersion = request.OsVersion,
            AppVersion = request.AppVersion,
            NotificationsEnabled = request.NotificationsEnabled
        });
        return Ok(result);
    }
}

public record RegisterMobileDeviceRequest
{
    public MobilePlatform Platform { get; init; }
    public string? DeviceToken { get; init; }
    public string? DeviceModel { get; init; }
    public string? OsVersion { get; init; }
    public string? AppVersion { get; init; }
    public bool NotificationsEnabled { get; init; }
}
