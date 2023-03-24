namespace KitchenInventory.Client.Services;

using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

/// <summary>
/// REST API Client for authentication.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IMessageService _messageService;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Creates a new instance of AuthService.
    /// </summary>
    /// <param name="messageService">Filled from DI container.</param>
    /// <param name="httpClient">Filled from DI container.</param>
    public AuthService(IMessageService messageService, HttpClient httpClient)
    {
        _messageService = messageService;
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var resp = await _httpClient.PostAsJsonAsync<LoginDTO>("/api/auth/login", new(username, password));
            switch (resp.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.Unauthorized:
                    return false;
                default:
                    _messageService.Send(MessageLevel.Error, $"Failed to authenticate: {resp.StatusCode} {resp.ReasonPhrase}.");
                    return false;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Failed to authenticate: {ex.GetType()} {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> LogoutAsync()
    {
        try
        {
            var resp = await _httpClient.PostAsync("/api/auth/logout", null);
            return resp.IsSuccessStatusCode;
        }
        catch(Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Failed to log out: {ex.GetType()} {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<string?> CurrentAsync()
    {
        try
        {
            var resp = await _httpClient.GetFromJsonAsync<UserInfo>("/api/auth/current");
            return resp?.Name;
        }
        catch
        {
            return null;
        }
    }
}
