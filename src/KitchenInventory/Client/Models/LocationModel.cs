namespace KitchenInventory.Client.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Describes a location. Used for user interfaces.
/// </summary>
public class LocationModel
{
    /// <summary>
    /// Name of the location.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Display color of the location.
    /// </summary>
    [Required]
    public string Color { get; set; } = "#0033ff";
}
