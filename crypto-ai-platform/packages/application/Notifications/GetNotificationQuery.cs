using MediatR;
using CryptoAIPlatform.Domain.Notifications;

namespace CryptoAIPlatform.Application.Notifications;

public record GetNotificationQuery : IRequest<GetNotificationResponse>
{
    public Guid NotificationId { get; init; }
    public Guid UserId { get; init; }
}

public record GetNotificationResponse(
    Guid NotificationId,
    Guid UserId,
    NotificationType Type,
    string Title,
    string Message,
    bool IsRead,
    DateTime? ReadAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);