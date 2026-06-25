using MediatR;
using System.Reflection;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, List<PermissionDto>>
{
    public Task<List<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = Enum.GetValues<Domain.IdentityAndAccess.Permission>()
            .Select(p => new PermissionDto(
                p,
                p.ToString(),
                GetPermissionDescription(p)))
            .ToList();

        return Task.FromResult(permissions);
    }

    private string GetPermissionDescription(Domain.IdentityAndAccess.Permission permission)
    {
        return permission switch
        {
            Domain.IdentityAndAccess.Permission.ViewUsers => "Visualizar usuários",
            Domain.IdentityAndAccess.Permission.CreateUsers => "Criar usuários",
            Domain.IdentityAndAccess.Permission.EditUsers => "Editar usuários",
            Domain.IdentityAndAccess.Permission.DeleteUsers => "Excluir usuários",
            Domain.IdentityAndAccess.Permission.ViewRoles => "Visualizar roles",
            Domain.IdentityAndAccess.Permission.CreateRoles => "Criar roles",
            Domain.IdentityAndAccess.Permission.EditRoles => "Editar roles",
            Domain.IdentityAndAccess.Permission.DeleteRoles => "Excluir roles",
            Domain.IdentityAndAccess.Permission.AssignRoles => "Atribuir roles",
            _ => permission.ToString()
        };
    }
}
