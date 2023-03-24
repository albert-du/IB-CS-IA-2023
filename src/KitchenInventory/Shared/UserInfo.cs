namespace KitchenInventory.Shared;

/// <summary>
/// Result from calling GET /api/auth/current.
/// </summary>
/// <param name="Name"></param>
/// <param name="Claims"></param>
public sealed record UserInfo(string? Name, Dictionary<string, string>? Claims);
