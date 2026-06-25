using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoAIPlatform.Application.Notifications;
using CryptoAIPlatform.Domain.Notifications;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/notifications")]
[ApiVersion("1.0")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
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
    [ProducesResponseType(typeof(List<GetNotificationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllNotifications([FromQuery] bool? onlyUnread)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetAllNotificationsQuery { UserId = userId, OnlyUnread = onlyUnread });
        return Ok(result);
    }

    [HttpGet("{notificationId:guid}")]
    [ProducesResponseType(typeof(GetNotificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotification(Guid notificationId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new GetNotificationQuery { NotificationId = notificationId, UserId = userId });
        return Ok(result);
    }

    [HttpPatch("{notificationId:guid}/read")]
    [ProducesResponseType(typeof(MarkNotificationAsReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsRead(Guid notificationId)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new MarkNotificationAsReadCommand { NotificationId = notificationId, UserId = userId });
        return Ok(result);
    }
}
