using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.IdentityAndAccess;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Presentation.Controllers;

/// <summary>
/// Controlador de autenticação e autorização. Gerencia usuários, papéis e permissões.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registra um novo usuário na plataforma.
    /// </summary>
    /// <param name="command">Dados do usuário para registro.</param>
    /// <returns>Usuário recém-criado.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = result.UserId }, result);
    }

    /// <summary>
    /// Realiza login de um usuário existente.
    /// </summary>
    /// <param name="command">Credenciais de login.</param>
    /// <returns>Token de autenticação e dados do usuário.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Cria um novo papel (role) no sistema.
    /// </summary>
    /// <param name="command">Dados do papel a ser criado.</param>
    /// <returns>Papel recém-criado.</returns>
    [HttpPost("roles")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateRoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateRole), new { id = result.RoleId }, result);
    }

    /// <summary>
    /// Obtém todos os papéis (roles) do sistema.
    /// </summary>
    /// <returns>Lista de papéis.</returns>
    [HttpGet("roles")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<GetRoleByIdResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _mediator.Send(new GetAllRolesQuery());
        return Ok(result);
    }

    /// <summary>
    /// Obtém um papel (role) por ID.
    /// </summary>
    /// <param name="roleId">ID do papel.</param>
    /// <returns>Dados do papel.</returns>
    [HttpGet("roles/{roleId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(GetRoleByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoleById(Guid roleId)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(roleId));
        return Ok(result);
    }

    /// <summary>
    /// Atualiza os dados de um papel (role).
    /// </summary>
    /// <param name="roleId">ID do papel.</param>
    /// <param name="command">Novos dados do papel.</param>
    /// <returns>Papel atualizado.</returns>
    [HttpPut("roles/{roleId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(GetRoleByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRole(Guid roleId, [FromBody] UpdateRoleCommand command)
    {
        command = command with { RoleId = roleId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Remove um papel (role) do sistema.
    /// </summary>
    /// <param name="roleId">ID do papel a ser removido.</param>
    [HttpDelete("roles/{roleId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        var result = await _mediator.Send(new DeleteRoleCommand(roleId));
        return Ok(result);
    }

    /// <summary>
    /// Atribui uma permissão a um papel (role).
    /// </summary>
    /// <param name="roleId">ID do papel.</param>
    /// <param name="command">Permissão a ser atribuída.</param>
    [HttpPost("roles/{roleId:guid}/permissions")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignPermissionToRole(Guid roleId, [FromBody] AssignPermissionToRoleCommand command)
    {
        command = command with { RoleId = roleId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Remove uma permissão de um papel (role).
    /// </summary>
    /// <param name="roleId">ID do papel.</param>
    /// <param name="permission">Permissão a ser removida.</param>
    [HttpDelete("roles/{roleId:guid}/permissions/{permission}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePermissionFromRole(Guid roleId, Permission permission)
    {
        var result = await _mediator.Send(new RemovePermissionFromRoleCommand(roleId, permission));
        return Ok(result);
    }

    /// <summary>
    /// Obtém todas as permissões disponíveis no sistema.
    /// </summary>
    /// <returns>Lista de permissões.</returns>
    [HttpGet("permissions")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPermissions()
    {
        var result = await _mediator.Send(new GetAllPermissionsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Atribui um papel (role) a um usuário.
    /// </summary>
    /// <param name="userId">ID do usuário.</param>
    /// <param name="command">Dados da atribuição.</param>
    [HttpPost("users/{userId:guid}/roles")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(AssignRoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignRole(Guid userId, [FromBody] AssignRoleCommand command)
    {
        command = command with { UserId = userId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Endpoint de teste para acesso de administrador.
    /// </summary>
    [HttpGet("admin/test")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult AdminTest()
    {
        return Ok("Admin access granted!");
    }

    /// <summary>
    /// Endpoint de teste para acesso de usuário autenticado.
    /// </summary>
    [HttpGet("user/test")]
    [Authorize]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult UserTest()
    {
        return Ok("User access granted!");
    }

    /// <summary>
    /// Obtém todos os usuários do sistema.
    /// </summary>
    /// <returns>Lista de usuários.</returns>
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<GetUserByIdResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    /// <summary>
    /// Obtém um usuário por ID.
    /// </summary>
    /// <param name="userId">ID do usuário.</param>
    /// <returns>Dados do usuário.</returns>
    [HttpGet("users/{userId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(userId));
        return Ok(result);
    }

    /// <summary>
    /// Atualiza os dados de um usuário.
    /// </summary>
    /// <param name="userId">ID do usuário.</param>
    /// <param name="command">Novos dados do usuário.</param>
    /// <returns>Usuário atualizado.</returns>
    [HttpPut("users/{userId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserCommand command)
    {
        command = command with { UserId = userId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Remove um usuário do sistema.
    /// </summary>
    /// <param name="userId">ID do usuário a ser removido.</param>
    [HttpDelete("users/{userId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var result = await _mediator.Send(new DeleteUserCommand(userId));
        return Ok(result);
    }
}