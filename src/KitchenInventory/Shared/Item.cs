namespace KitchenInventory.Shared;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an item in the inventory.
/// </summary>
public sealed class Item
{
    /// <summary>
    /// The item's unique identifier.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Name of the item.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Description of the item.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Creation time and date of the item.
    /// </summary>
    public required DateTime CreationDate { get; set; }

    /// <summary>
    /// Date at which the item will expire.
    /// </summary>
    public DateOnly? ExpirationDate { get; set; }
    
    /// <summary>
    /// Whether or not the item has been marked deleted (usually eaten).
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// The (optional) id of the category of the item.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// The (optional) category of the item.
    /// </summary>
    [JsonIgnore]
    public Category? Category { get; set; }

    /// <summary>
    /// The (optional) Id of the location of the item.
    /// </summary>
    /// <value></value>
    public Guid? LocationId { get; set; }

    /// <summary>
    /// The (optional) location of the item.
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public Location? Location { get; set; }
}