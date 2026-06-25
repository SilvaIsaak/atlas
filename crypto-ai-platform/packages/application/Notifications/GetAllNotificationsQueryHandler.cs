using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Notifications;

public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, List<GetNotificationResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllNotificationsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetNotificationResponse>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Notifications.Where(n => n.UserId == request.UserId);

        if (request.OnlyUnread.HasValue)
        {
            query = query.Where(n => n.IsRead == !request.OnlyUnread.Value);
        }

        var notifications = await query
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new GetNotificationResponse(
                n.Id,
                n.UserId,
                n.Type,
                n.Title,
                n.Message,
                n.IsRead,
                n.ReadAt,
                n.CreatedAt,
                n.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return notifications;
    }
}