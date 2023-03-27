namespace KitchenInventory.Server.Services;

using Microsoft.Extensions.Options;

/// <summary>
/// Authentication service with username and password from configuration file.
/// </summary>
public class AuthService : IAuthService
{
    private readonly AuthCredentialsOptions options;

    /// <summary>
    /// Creates a new instance of AuthService.
    /// </summary>
    /// <param name="authCredentialsOptions"></param>
    public AuthService(IOptions<AuthCredentialsOptions> authCredentialsOptions)
    {
        options = authCredentialsOptions.Value;
    }

    /// <inheritdoc/>
    public Task<bool> AuthenticateAsync(string username, string password) =>
        Task.FromResult(options.Username == username && options.Password == password);
}