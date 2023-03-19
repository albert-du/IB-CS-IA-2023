namespace KitchenInventory.Shared;

using System.Text.Json.Serialization;

/// <summary>
/// Category of an item.
/// A group of items that may share a food group or other characteristic.
/// </summary>
public sealed class Category
{
    /// <summary>
    /// The Category's unique identifier.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The Category's name.
    /// </summary>
    public required string Name { get; set; } = string.Empty;

    /// <summary>
    /// The display color of the category.
    /// </summary>
    public string Color { get; set; } = "#38bdf8";

    /// <summary>
    /// The Items that belong within the Category.
    /// </summary>
    [JsonIgnore]
    public ICollection<Item> Items { get; set; } = new List<Item>();
}