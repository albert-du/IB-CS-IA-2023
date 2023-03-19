namespace KitchenInventory.Server.Services;

/// <summary>
/// Mock authentication service with hardcoded login credentials.
/// </summary>
public class MockAuthService : IAuthService
{
    /// <inheritdoc/>
    public Task<bool> AuthenticateAsync(string username, string password) =>
        Task.FromResult(username == "admin" && password == "admin");
}