using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<GetRoleByIdResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllRolesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetRoleByIdResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _dbContext.Roles
            .Include(r => r.RolePermissions)
            .ToListAsync(cancellationToken);

        return roles.Select(r => new GetRoleByIdResponse(
            r.Id,
            r.Name!,
            r.Description,
            r.CreatedAt,
            r.RolePermissions.Select(rp => rp.Permission).ToList()
        )).ToList();
    }
}
