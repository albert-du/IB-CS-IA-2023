namespace KitchenInventory.Client.Services;

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;
    private readonly IMessageService _messageService;
    private string? _currentUser;

    public CustomAuthStateProvider(IAuthService authService, IMessageService messageService)
    {
        _authService = authService;
        _messageService = messageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity = new();
        try
        {
            var userInfo = await CurrentAsync();
            if (userInfo is not null)
            {
                Claim[] claims = { new(ClaimTypes.Name, userInfo) };
                identity = new(claims, "Server Authentication");
            }
        }
        catch (HttpRequestException)
        {
            _messageService.Send(MessageLevel.Error, "Failed to connect to server.");
        }
        return new(new(identity));
    }

    private async Task<string?> CurrentAsync()
    {
        if (_currentUser is null)
            _currentUser = await _authService.CurrentAsync();
        return _currentUser;
    }

    public async Task Logout()
    {
        await _authService.LogoutAsync();
        _currentUser = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<bool> Login(string username, string password)
    {
        var res = await _authService.LoginAsync(username, password);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        return res;
    }
}