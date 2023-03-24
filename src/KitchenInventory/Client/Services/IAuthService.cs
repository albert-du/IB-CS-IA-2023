namespace KitchenInventory.Client.Services;

/// <summary>
/// Authentication Service for logging in, out, and managing user state.
/// </summary>
public interface IAuthService
{

    /// <summary>
    /// Logs into the application.
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <returns>Success of the login.</returns>
    Task<bool> LoginAsync(string username, string password);
    
    /// <summary>
    /// Logs out of the application.
    /// </summary>
    /// <returns>Success of the logout.</returns>
    Task<bool> LogoutAsync();

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <returns>The user's name if successful, null otherwise.</returns>
    Task<string?> CurrentAsync();
}
