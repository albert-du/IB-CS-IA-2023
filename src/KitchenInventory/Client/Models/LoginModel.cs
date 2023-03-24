namespace KitchenInventory.Client.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents the login model.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Login username.
    /// </summary>
    [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Login password.
    /// </summary>
    /// <value></value>    
    [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
    public string Password { get; set; } = string.Empty;
}