namespace KitchenInventory.Server.Models;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Database context for items.
/// </summary>
public sealed class ItemContext : DbContext
{
    /// <summary>
    /// Creates a new database context.
    /// </summary>
    /// <param name="options"></param>
    public ItemContext(DbContextOptions<ItemContext> options) : base(options)
    {
    }

    /// <summary>
    /// Items.
    /// </summary>
    public DbSet<Item> Items { get; set; } = default!;
    
    /// <summary>
    /// Categories.
    /// </summary>
    public DbSet<Category> Categories { get; set; } = default!;

    /// <summary>
    /// Locations.
    /// </summary>
    public DbSet<Location> Locations { get; set; } = default!;
}