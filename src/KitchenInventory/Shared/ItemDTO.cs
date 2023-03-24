namespace KitchenInventory.Shared;

/// <summary>
/// Item Domain Transfer Object.
/// </summary>
/// <param name="Name">The name of the item.</param>
/// <param name="Description">The description of the item.</param>
/// <param name="Expiration">The expiration date of the item.</param>
/// <param name="Category">The optional category of the item.</param>
/// <param name="Location">The optional location of the item.</param>
/// <param name="Deleted">The deletion status of the item.</param>
/// <returns>A new domain transfer object</returns>
public sealed record ItemDTO(string Name, string? Description, DateOnly? Expiration, Guid? Category, Guid? Location, bool Deleted = false);