namespace KitchenInventory.Client.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Describes a category. Used for user interfaces.
/// </summary>
public class CategoryModel
{
    /// <summary>
    /// Name of category.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Display color of category.
    /// </summary>
    [Required]
    public string Color { get; set; } = "#0033ff";
}

