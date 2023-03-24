namespace KitchenInventory.Client.Services;

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

/// <summary>
/// Provides authentication state support.
/// </summary>
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;
    private readonly IMessageService _messageService;
    private string? _currentUser;

    /// <summary>
    /// Creates a new 
    /// </summary>
    /// <param name="authService">From DI container.</param>
    /// <param name="messageService">From DI container.</param>
    public CustomAuthStateProvider(IAuthService authService, IMessageService messageService)
    {
        _authService = authService;
        _messageService = messageService;
    }

    /// <inheritdoc/>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity = new();
        try
        {
            var userInfo = await CurrentAsync();
            if (userInfo is not null)
            {
                Claim[] claims = { new(ClaimTypes.Name, userInfo) };
                identity = new(claims, "auth");
            }
        }
        catch (HttpRequestException)
        {
            _messageService.Send(MessageLevel.Error, "Failed to connect to server.");
        }
        return new(new(identity));
    }

    /// <inheritdoc/>
    private async Task<string?> CurrentAsync()
    {
        if (_currentUser is null)
            _currentUser = await _authService.CurrentAsync();
        return _currentUser;
    }

    /// <inheritdoc/>
    public async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        _currentUser = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <inheritdoc/>
    public async Task<bool> LoginAsync(string username, string password)
    {
        var res = await _authService.LoginAsync(username, password);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        return res;
    }
}