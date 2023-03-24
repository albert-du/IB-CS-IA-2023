namespace KitchenInventory.Server.Models;

/// <summary>
/// Options for configuring JSON Web Token authentication.
/// </summary>
public class AuthCredentialsOptions
{
    /// <summary>
    /// Auth Credentials Constant Name.
    /// </summary>
    public const string Credentials = nameof(Credentials);

    /// <summary>
    /// Username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}