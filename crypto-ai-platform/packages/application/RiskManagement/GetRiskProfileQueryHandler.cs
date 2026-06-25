using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.RiskManagement;

public class GetRiskProfileQueryHandler : IRequestHandler<GetRiskProfileQuery, GetRiskProfileResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetRiskProfileQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetRiskProfileResponse> Handle(GetRiskProfileQuery request, CancellationToken cancellationToken)
    {
        var riskProfile = await _dbContext.RiskProfiles
            .FirstOrDefaultAsync(rp => rp.Id == request.RiskProfileId && rp.UserId == request.UserId, cancellationToken);

        if (riskProfile == null)
        {
            throw new KeyNotFoundException("RiskProfile not found");
        }

        return new GetRiskProfileResponse(
            riskProfile.Id,
            riskProfile.UserId,
            riskProfile.Name,
            riskProfile.Rules,
            riskProfile.Alerts,
            riskProfile.IsActive,
            riskProfile.CreatedAt,
            riskProfile.UpdatedAt
        );
    }
}