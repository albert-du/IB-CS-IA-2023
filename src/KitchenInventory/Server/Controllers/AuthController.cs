namespace KitchenInventory.Server.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using KitchenInventory.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

/// <summary>
/// Controller for authenticating users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JWTOptions _jwtOptions;

    /// <summary>
    /// Creates a new AuthController
    /// </summary>
    /// <param name="authService">From DI container.</param>
    /// <param name="jwtOptions">From DI container.</param>
    public AuthController(IAuthService authService, IOptions<JWTOptions> jwtOptions)
    {
        _authService = authService;
        _jwtOptions = jwtOptions.Value;
    }

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    /// <param name="request">Login credentials.</param>
    /// <response code="200">JWT Auth Token.</response>
    /// <response code="401">Invalid Credentials.</response>
    /// <returns>JWT Token.</returns>
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] LoginDTO request)
    {
        if (await _authService.AuthenticateAsync(request.Username, request.Password))
        {
            List<Claim> claims = new()
            {
                new(JwtRegisteredClaimNames.Name, request.Username),
            };
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken token = new(claims: claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);
            return Ok(new LoginTokenResponse(new JwtSecurityTokenHandler().WriteToken(token)));
        }
        return Unauthorized();
    }

    /// <summary>
    /// Attempts to login and write the auth cookie.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO request)
    {
        if (await _authService.AuthenticateAsync(request.Username, request.Password))
        {
            List<Claim> claims = new()
            {
                new (ClaimTypes.Name, request.Username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = true });
            return Ok();
        }
        return Unauthorized();
    }

    /// <summary>
    /// Logs out.
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    public async Task LogoutAsync()
    {
        await HttpContext.SignOutAsync();
    }

    /// <summary>
    /// Gets available information on the current user.
    /// </summary>
    /// <returns></returns>
    [HttpGet("current")]
    public IActionResult CurrentUserInfo() =>
        User?.Identity?.IsAuthenticated is true
        ? Ok(new UserInfo(User?.Identity?.Name, User?.Claims.ToDictionary(c => c.Type, c => c.Value)))
        : Unauthorized();
}