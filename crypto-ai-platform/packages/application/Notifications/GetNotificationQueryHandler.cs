using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Notifications;

public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, GetNotificationResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetNotificationQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetNotificationResponse> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        var notification = await _dbContext.Notifications
            .FirstOrDefaultAsync(n => n.Id == request.NotificationId && n.UserId == request.UserId, cancellationToken);

        if (notification == null)
        {
            throw new KeyNotFoundException("Notification not found");
        }

        return new GetNotificationResponse(
            notification.Id,
            notification.UserId,
            notification.Type,
            notification.Title,
            notification.Message,
            notification.IsRead,
            notification.ReadAt,
            notification.CreatedAt,
            notification.UpdatedAt
        );
    }
}