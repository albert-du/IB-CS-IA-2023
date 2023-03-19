namespace KitchenInventory.Shared;

/// <summary>
/// Category Domain Transfer Object.
/// </summary>
/// <param name="Color">Display color of the Category.</param>
/// <param name="Name">Name of the Category.</param>
/// <returns>A new domain transfer object.</returns>
public sealed record CategoryDTO(string Color, string Name);