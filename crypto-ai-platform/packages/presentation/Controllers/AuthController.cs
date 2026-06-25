using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.IdentityAndAccess;
using CryptoAIPlatform.Application.Wallets;

namespace CryptoAIPlatform.Presentation.Controllers;

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

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = result.UserId }, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("roles")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateRoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateRole), new { id = result.RoleId }, result);
    }

    [HttpGet("roles")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<GetRoleByIdResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _mediator.Send(new GetAllRolesQuery());
        return Ok(result);
    }

    [HttpGet("roles/{roleId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(GetRoleByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoleById(Guid roleId)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(roleId));
        return Ok(result);
    }

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

    [HttpDelete("roles/{roleId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        var result = await _mediator.Send(new DeleteRoleCommand(roleId));
        return Ok(result);
    }

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

    [HttpDelete("roles/{roleId:guid}/permissions/{permission}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePermissionFromRole(Guid roleId, Permission permission)
    {
        var result = await _mediator.Send(new RemovePermissionFromRoleCommand(roleId, permission));
        return Ok(result);
    }

    [HttpGet("permissions")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPermissions()
    {
        var result = await _mediator.Send(new GetAllPermissionsQuery());
        return Ok(result);
    }

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

    [HttpGet("admin/test")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult AdminTest()
    {
        return Ok("Admin access granted!");
    }

    [HttpGet("user/test")]
    [Authorize]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult UserTest()
    {
        return Ok("User access granted!");
    }

    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<GetUserByIdResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [HttpGet("users/{userId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(userId));
        return Ok(result);
    }

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

    [HttpDelete("users/{userId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var result = await _mediator.Send(new DeleteUserCommand(userId));
        return Ok(result);
    }

    [HttpGet("exchanges")]
    [Authorize]
    [ProducesResponseType(typeof(List<ExchangeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExchanges()
    {
        var exchanges = new List<ExchangeDto>
        {
            new ExchangeDto(Guid.NewGuid(), "Binance", "BINANCE", "https://api.binance.com", "wss://stream.binance.com:9443", true)
        };
        return Ok(exchanges);
    }

    [HttpGet("users/{userId:guid}/exchanges")]
    [Authorize]
    [ProducesResponseType(typeof(List<ExchangeIntegrationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserExchangeIntegrations(Guid userId)
    {
        var result = await _mediator.Send(new GetUserExchangeIntegrationsQuery(userId));
        return Ok(result);
    }

    [HttpPost("users/{userId:guid}/exchanges")]
    [Authorize]
    [ProducesResponseType(typeof(ExchangeIntegrationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateExchangeIntegration(Guid userId, [FromBody] CreateExchangeIntegrationRequest request)
    {
        var command = new CreateExchangeIntegrationCommand(userId, request.ExchangeId, request.ApiKey, request.ApiSecret, request.Passphrase);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserExchangeIntegrations), new { userId }, result);
    }

    [HttpGet("users/{userId:guid}/wallets")]
    [Authorize]
    [ProducesResponseType(typeof(WalletDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWallet(Guid userId, [FromQuery] Guid? exchangeIntegrationId = null)
    {
        var result = await _mediator.Send(new GetWalletQuery(userId, exchangeIntegrationId));
        return Ok(result);
    }

    [HttpPost("users/{userId:guid}/exchanges/{exchangeIntegrationId:guid}/wallets/sync")]
    [Authorize]
    [ProducesResponseType(typeof(WalletDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> SyncWalletBalances(Guid userId, Guid exchangeIntegrationId)
    {
        var result = await _mediator.Send(new SyncWalletBalancesCommand(userId, exchangeIntegrationId));
        return Ok(result);
    }
}

public record CreateExchangeIntegrationRequest(Guid ExchangeId, string ApiKey, string ApiSecret, string? Passphrase = null);

public record ExchangeDto(Guid Id, string Name, string Code, string ApiBaseUrl, string WsUrl, bool IsActive);