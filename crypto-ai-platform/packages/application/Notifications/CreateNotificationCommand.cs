using MediatR;
using CryptoAIPlatform.Domain.Notifications;

namespace CryptoAIPlatform.Application.Notifications;

public record CreateNotificationCommand : IRequest<CreateNotificationResponse>
{
    public Guid UserId { get; init; }
    public NotificationType Type { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

public record CreateNotificationResponse
{
    public Guid NotificationId { get; init; }
    public bool IsRead { get; init; }
}