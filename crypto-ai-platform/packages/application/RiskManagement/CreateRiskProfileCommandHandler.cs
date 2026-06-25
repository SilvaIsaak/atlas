using MediatR;
using CryptoAIPlatform.Domain.RiskManagement;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.RiskManagement;

public class CreateRiskProfileCommandHandler : IRequestHandler<CreateRiskProfileCommand, CreateRiskProfileResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateRiskProfileCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateRiskProfileResponse> Handle(CreateRiskProfileCommand request, CancellationToken cancellationToken)
    {
        var riskProfile = new RiskProfile
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Rules = request.Rules ?? new List<RiskRule>(),
            IsActive = true
        };

        await _dbContext.RiskProfiles.AddAsync(riskProfile, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateRiskProfileResponse
        {
            RiskProfileId = riskProfile.Id,
            Name = riskProfile.Name
        };
    }
}