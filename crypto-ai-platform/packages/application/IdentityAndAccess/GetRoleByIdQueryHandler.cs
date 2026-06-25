using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleByIdResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetRoleByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetRoleByIdResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);
        
        if (role == null)
            throw new InvalidOperationException("Role não encontrada.");

        return new GetRoleByIdResponse(
            role.Id,
            role.Name!,
            role.Description,
            role.CreatedAt,
            role.RolePermissions.Select(rp => rp.Permission).ToList()
        );
    }
}
