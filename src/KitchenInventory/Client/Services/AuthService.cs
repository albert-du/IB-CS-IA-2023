namespace KitchenInventory.Client.Services;

using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    private readonly IMessageService _messageService;
    private readonly HttpClient _httpClient;

    public AuthService(IMessageService messageService, HttpClient httpClient)
    {
        _messageService = messageService;
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var resp = await _httpClient.PostAsJsonAsync<LoginDTO>("/auth/login", new(username, password));
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

    public async Task<bool> LogoutAsync()
    {
        var resp = await _httpClient.PostAsync("/auth/logout", null);
        return resp.IsSuccessStatusCode;
    }

    public async Task<string?> CurrentAsync()
    {
        var resp = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>("/auth/current");
        return resp?["UserName"];
    }
}
