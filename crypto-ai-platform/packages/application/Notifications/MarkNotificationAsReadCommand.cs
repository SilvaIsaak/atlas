using MediatR;

namespace CryptoAIPlatform.Application.Notifications;

public record MarkNotificationAsReadCommand : IRequest<MarkNotificationAsReadResponse>
{
    public Guid NotificationId { get; init; }
    public Guid UserId { get; init; }
}

public record MarkNotificationAsReadResponse
{
    public Guid NotificationId { get; init; }
    public bool IsRead { get; init; }
}