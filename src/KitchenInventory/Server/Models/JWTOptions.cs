namespace KitchenInventory.Server.Models;

/// <summary>
/// Options for configuring JSON Web Token authentication.
/// </summary>
public class JWTOptions
{
    /// <summary>
    /// JWT Constant Name.
    /// </summary>
    public const string JWT = "JWT";

    /// <summary>
    /// JWT Secret.
    /// </summary>
    public string Secret { get; set; } = string.Empty;
}