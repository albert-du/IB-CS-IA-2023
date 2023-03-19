namespace KitchenInventory.Server.Controllers;

using KitchenInventory.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controller for the management of items.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class ItemController : ControllerBase
{
    private readonly ItemContext _itemContext;

    /// <summary>
    /// Creates a new ItemController.
    /// </summary>
    /// <param name="itemContext">From DI container.</param>
    public ItemController(ItemContext itemContext)
    {
        _itemContext = itemContext;
    }

    /// <summary>
    /// Creates a new Item.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(ItemDTO request)
    {
        // Ensure that the Location and Category if requested actually exist.
        if (request.Location is not null && await _itemContext.Locations.FindAsync(request.Location) is null)
            return BadRequest("Location doesn't exist");

        if (request.Category is not null && await _itemContext.Categories.FindAsync(request.Category) is null)
            return BadRequest("Category doesn't exist");

        // Generate a Item object.
        var id = Guid.NewGuid();
        Item item = new()
        {
            CategoryId = request.Category,
            LocationId = request.Location,
            Id = id,
            Name = request.Name,
            Description = request.Description ?? string.Empty,
            CreationDate = DateTime.UtcNow,
            ExpirationDate = request.Expiration,
            Deleted = request.Deleted
        };

        // Add item to the database context.
        await _itemContext.Items.AddAsync(item);

        // Persist changes to the database.
        await _itemContext.SaveChangesAsync();

        // Return new object with its location.
        return CreatedAtAction(nameof(ReadAsync), new { id }, item);
    }

    /// <summary>
    /// Reads a Item by id and returns it as JSON.
    /// </summary>
    /// <param name="id">Item Id.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadAsync(Guid id) =>
        // Return the Item with the id if it exists.
        await _itemContext.Items.FindAsync(id) switch
        {
            Item item => Ok(item),
            _ => NotFound(),
        };

    /// <summary>
    /// Modifies an Item.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ItemDTO request)
    {
        var item = await _itemContext.Items.FindAsync(id);
        if (item is null)
            return NotFound();

        // Check that the location and category requested actually exist.
        if (request.Location is not null && await _itemContext.Locations.FindAsync(request.Location) is null)
            return BadRequest("Location doesn't exist");

        if (request.Category is not null && await _itemContext.Categories.FindAsync(request.Category) is null)
            return BadRequest("Category doesn't exist");

        item.Name = request.Name;
        item.Description = request.Description ?? string.Empty;
        item.ExpirationDate = request.Expiration;
        item.Deleted = request.Deleted;
        item.LocationId = request.Location;
        item.CategoryId = request.Category;

        await _itemContext.SaveChangesAsync();

        return Ok(item);
    }

    /// <summary>
    /// Deletes an Item.
    /// </summary>
    /// <param name="id">Item Id.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // Try the find the item.
        var item = await _itemContext.Items.FindAsync(id);
        if (item is null)
            // Return 404 if it doens't exist.
            return NotFound();

        // Remove the item from the data context.
        _itemContext.Items.Remove(item);
        await _itemContext.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Searches for an item with a given term.
    /// </summary>
    /// <param name="q">Keyword</param>
    /// <param name="page">Page number</param>
    /// <param name="count">Number to display on a page</param>
    /// <param name="location">Location</param>
    /// <param name="category">Category</param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<PaginatedResult<Item>> SearchAsync(string? q = null, int page = 1, int count = 50, Guid? location = null, Guid? category = null)
    {
        // Build the linq query.
        var query =
            (location, category) switch
            {
                // Linq query with the location without the category.
                (Guid _, null) => _itemContext.Locations
                                              .AsNoTracking()
                                              .Where(x => x.Id == location) // Find the location.
                                              .Take(1)                      // Truncate to one.
                                              .SelectMany(x => x.Items),    // Select the items.
                // Linq query with the category without the location.
                (null, Guid _) => _itemContext.Categories
                                              .AsNoTracking()
                                              .Where(x => x.Id == category) // Find the category.
                                              .Take(1)                      // Truncate to one.
                                              .SelectMany(x => x.Items),    // Select the items.
                // Linq query with both the category and the location.
                (Guid _, Guid _) => _itemContext.Locations
                                                .AsNoTracking()
                                                .Where(x => x.Id == location)          // Find the location.
                                                .Take(1)                               // Truncate to one.
                                                .SelectMany(x => x.Items)              // Select the items.
                                                .Where(x => x.CategoryId == category), // Filter the items by category.

                _ => _itemContext.Items
            };

        // Remove whitespace from the start and end of the keyword.
        q = q?.Trim();

        // Add keyword searching to the query.
        query =
            string.IsNullOrWhiteSpace(q)
            ? query.Where(x => !x.Deleted)
            : query.Where(x => !x.Deleted && (EF.Functions.Like(x.Name, $"%{q}%") || EF.Functions.Like(x.Description, $"%{q}%")));
        // Paginate and return.
        return await query.OrderByDescending(x => x.CreationDate).PaginateAsync(page, count);
    }

    /// <summary>
    /// Reads all items given pagination.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="count">Number to display per page.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PaginatedResult<Item>> GetAsync(int page = 1, int count = 50) =>
        await _itemContext.Items
                          .AsNoTracking()
                          .Where(x => !x.Deleted) // Check for only not deleted items.
                          .OrderByDescending(x => x.CreationDate) // Newest (greatest dates) first.
                          .PaginateAsync(page, count);
    
    /// <summary>
    /// Gets the items closest to expiration
    /// </summary>
    /// <param name="page"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [HttpGet("expiring")]
    public async Task<PaginatedResult<Item>> ExpiringAsync(int page = 1, int count = 50) =>
        await _itemContext.Items
                          .AsNoTracking()
                          .Where(x => !x.Deleted &&
                                      x.ExpirationDate.HasValue &&
                                      (DateTime.UtcNow - x.ExpirationDate.Value.ToDateTime(TimeOnly.MinValue)) < TimeSpan.FromDays(7))
                          .OrderBy(x => x.ExpirationDate)
                          .PaginateAsync(page, count);

    /// <summary>
    /// Gets items that have been soft deleted.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [HttpGet("history")]
    public async Task<PaginatedResult<Item>> HistoryAsync(int page = 1, int count = 50) =>
        await _itemContext.Items
                          .AsNoTracking()
                          .Where(x => x.Deleted)
                          .OrderByDescending(x => x.CreationDate)
                          .PaginateAsync(page, count);
}