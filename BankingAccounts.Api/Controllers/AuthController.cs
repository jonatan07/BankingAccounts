using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using BankingAccounts.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BankingAccounts.Api.Controllers;

/// <summary>
/// Expone operaciones de autenticacion basica mediante JWT.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/auth")]
[Produces("application/json")]
public sealed class AuthController(IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Genera un token JWT para credenciales validas.
    /// </summary>
    /// <param name="request">Credenciales de acceso configuradas para la aplicacion.</param>
    /// <returns>Token JWT y fecha de expiracion en UTC.</returns>
    /// <response code="200">Credenciales validas; retorna el token de acceso.</response>
    /// <response code="400">La solicitud contiene datos invalidos.</response>
    /// <response code="401">Las credenciales no son validas.</response>
    [HttpPost("token")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<TokenResponse> Login(LoginRequest request)
    {
        var configuredUser = configuration["Jwt:Username"];
        var configuredPassword = configuration["Jwt:Password"];

        if (request.Username != configuredUser || request.Password != configuredPassword)
        {
            return Unauthorized(new { Error = "Invalid credentials." });
        }

        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAtUtc = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            [new Claim(ClaimTypes.Name, request.Username)],
            expires: expiresAtUtc,
            signingCredentials: credentials);

        return Ok(new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), expiresAtUtc));
    }
}
