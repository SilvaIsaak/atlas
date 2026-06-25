using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.RiskManagement;

public class GetAllRiskProfilesQueryHandler : IRequestHandler<GetAllRiskProfilesQuery, List<GetRiskProfileResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllRiskProfilesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetRiskProfileResponse>> Handle(GetAllRiskProfilesQuery request, CancellationToken cancellationToken)
    {
        var riskProfiles = await _dbContext.RiskProfiles
            .Where(rp => rp.UserId == request.UserId)
            .OrderByDescending(rp => rp.CreatedAt)
            .Select(rp => new GetRiskProfileResponse(
                rp.Id,
                rp.UserId,
                rp.Name,
                rp.Rules,
                rp.Alerts,
                rp.IsActive,
                rp.CreatedAt,
                rp.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return riskProfiles;
    }
}