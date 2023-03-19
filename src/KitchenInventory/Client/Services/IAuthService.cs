namespace KitchenInventory.Client.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password);
    Task<bool> LogoutAsync();
    Task<string?> CurrentAsync();
}
