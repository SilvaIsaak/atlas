using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Monitoring;

public class AcknowledgeSystemAlertCommandHandler : IRequestHandler<AcknowledgeSystemAlertCommand, AcknowledgeSystemAlertResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public AcknowledgeSystemAlertCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AcknowledgeSystemAlertResponse> Handle(AcknowledgeSystemAlertCommand request, CancellationToken cancellationToken)
    {
        var alert = await _dbContext.SystemAlerts
            .FirstOrDefaultAsync(a => a.Id == request.AlertId, cancellationToken);

        if (alert == null)
        {
            throw new KeyNotFoundException("System alert not found");
        }

        alert.Acknowledged = true;
        alert.AcknowledgedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AcknowledgeSystemAlertResponse
        {
            AlertId = alert.Id,
            Acknowledged = alert.Acknowledged
        };
    }
}
