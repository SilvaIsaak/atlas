using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Infrastructure.Services;

public interface IJwtTokenService
{
    Task<(string AccessToken, string RefreshToken, List<string> Roles)> GenerateTokens(User user);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public JwtTokenService(IConfiguration configuration, UserManager<User> userManager, ApplicationDbContext dbContext)
    {
        _configuration = configuration;
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<(string AccessToken, string RefreshToken, List<string> Roles)> GenerateTokens(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);
        var userRoles = await _dbContext.Roles
            .Where(r => roles.Contains(r.Name!))
            .Include(r => r.RolePermissions)
            .ToListAsync();
        
        var permissions = userRoles
            .SelectMany(r => r.RolePermissions.Select(rp => rp.Permission.ToString()))
            .Distinct()
            .ToList();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        claims.AddRange(permissions.Select(perm => new Claim("Permission", perm)));

        var accessToken = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["AccessTokenExpirationMinutes"] ?? "15")),
            signingCredentials: signingCredentials);

        var refreshToken = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(double.Parse(jwtSettings["RefreshTokenExpirationDays"] ?? "7")),
            signingCredentials: signingCredentials);

        return (
            new JwtSecurityTokenHandler().WriteToken(accessToken),
            new JwtSecurityTokenHandler().WriteToken(refreshToken),
            roles.ToList());
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secretKey,
            ValidateLifetime = false, // Allow expired tokens here
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }

        return principal;
    }
}