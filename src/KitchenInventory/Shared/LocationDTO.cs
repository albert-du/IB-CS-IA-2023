namespace KitchenInventory.Shared;

/// <summary>
/// Location Domain Transfer Object.
/// </summary>
/// <param name="Color">Display color of the Location.</param>
/// <param name="Name">Name of the Location.</param>
/// <returns>A new domain transfer object.</returns>
public sealed record LocationDTO(string Color, string Name);