namespace KitchenInventory.Shared;

/// <summary>
/// Represents a successful login.
/// </summary>
/// <param name="Token">JWT API token.</param>
public sealed record LoginTokenResponse(string Token);
