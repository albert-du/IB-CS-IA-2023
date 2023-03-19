namespace KitchenInventory.Server.Services;

/// <summary>
/// Authenticates users.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates the user.
    /// </summary>
    /// <param name="username">The user's username</param>
    /// <param name="password">The user's password</param>
    /// <returns>True if credentials are valid.</returns>
    Task<bool> AuthenticateAsync(string username, string password);
}