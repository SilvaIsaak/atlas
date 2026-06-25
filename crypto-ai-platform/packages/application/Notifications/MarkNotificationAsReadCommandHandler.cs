using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Notifications;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, MarkNotificationAsReadResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public MarkNotificationAsReadCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MarkNotificationAsReadResponse> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _dbContext.Notifications
            .FirstOrDefaultAsync(n => n.Id == request.NotificationId && n.UserId == request.UserId, cancellationToken);

        if (notification == null)
        {
            throw new KeyNotFoundException("Notification not found");
        }

        notification.IsRead = true;
        notification.ReadAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new MarkNotificationAsReadResponse
        {
            NotificationId = notification.Id,
            IsRead = notification.IsRead
        };
    }
}