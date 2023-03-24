namespace KitchenInventory.Client.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Describes an item. Used for user interfaces.
/// </summary>
public class ItemModel
{
    /// <summary>
    /// Name of item.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of item.
    /// </summary>
    /// <value></value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Expiration Date of item. Represented here as a DateTime instead of DateOnly.
    /// </summary>
    /// <returns></returns>
    public DateTime Expiration { get; set; } = DateTime.Now.AddDays(7);

    /// <summary>
    /// The optional category guid string of the item.
    /// </summary>
    /// <remarks>
    /// This is a string because Razor components input form bindings don't support Guids.
    /// </remarks>
    public string? Category { get; set; }
    
    /// <summary>
    /// Category Id of the item.
    /// </summary>
    public Guid? CategoryId =>
        string.IsNullOrEmpty(Category)
        ? null
        : Guid.Parse(Category);

    /// <summary>
    /// Location Id of the item.
    /// </summary>
    public Guid? LocationId =>
        string.IsNullOrEmpty(Location)
        ? null
        : Guid.Parse(Location);

    /// <summary>
    /// The optional location guid string of the item.
    /// </summary>
    /// <remarks>
    /// This is a string because Razor components input form bindings don't support Guids.
    /// </remarks>
    public string? Location { get; set; }

    /// <summary>
    /// Deletion status.
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// Whether or not it has an expiration.
    /// </summary>
    /// <remarks>
    /// This is for showing on the UI.
    /// </remarks>
    public bool HasExpiration { get; set; }
}
