using MediatR;

namespace CryptoAIPlatform.Application.Notifications;

public record GetAllNotificationsQuery : IRequest<List<GetNotificationResponse>>
{
    public Guid UserId { get; init; }
    public bool? OnlyUnread { get; init; }
}