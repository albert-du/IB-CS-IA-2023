namespace KitchenInventory.Shared;

/// <summary>
/// Represents a login request.
/// </summary>
/// <param name="Username">Username of the user.</param>
/// <param name="Password">Password of the user.</param>
public sealed record LoginDTO(string Username, string Password);
