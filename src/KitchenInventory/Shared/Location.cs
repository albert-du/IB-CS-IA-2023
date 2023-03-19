namespace KitchenInventory.Shared;

using System.Text.Json.Serialization;

/// <summary>
/// A location where items are stored.
/// Examples might be: refrigerator, freezer, pantry, etc.
/// </summary>
public sealed class Location
{
    /// <summary>
    /// The Location's unique identifier.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The Location's name.
    /// </summary>
    public required string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The display color of the location.
    /// </summary>
    /// <value></value>
    public string Color { get; set; } = "#38bdf8";

    /// <summary>
    /// The items in that location.
    /// </summary>
    [JsonIgnore]
    public ICollection<Item> Items { get; set; } = new List<Item>();
}