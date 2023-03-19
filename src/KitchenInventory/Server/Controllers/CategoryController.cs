namespace KitchenInventory.Server.Controllers;

using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controller for management of categories.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class CategoryController : ControllerBase
{
    private readonly ItemContext _itemContext;

    /// <summary>
    /// Creates a new CategoryController.
    /// </summary>
    /// <param name="itemContext">From DI container.</param>
    public CategoryController(ItemContext itemContext)
    {
        _itemContext = itemContext;
    }

    /// <summary>
    /// Creates a new Category.
    /// </summary>
    /// <param name="request"></param>
    /// <response code="201">Category Created</response>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryDTO request)
    {
        // Ensure color is valid.
        try
        {
            ColorTranslator.FromHtml(request.Color);
        }
        catch
        {
            return BadRequest("Color is invalid.");
        }

        // Create a new Category object.
        var id = Guid.NewGuid();
        Category category = new()
        {
            Id = id,
            Color = request.Color,
            Name = request.Name
        };

        // Add the Category object to the database.
        await _itemContext.Categories.AddAsync(category);
        await _itemContext.SaveChangesAsync();

        // Return the new object.
        return CreatedAtAction(nameof(ReadAsync), new { id }, category);
    }

    /// <summary>
    /// Modifies a Category and returns it as JSON.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CategoryDTO request)
    {
        // Ensure color is valid.
        try
        {
            ColorTranslator.FromHtml(request.Color);
        }
        catch
        {
            return BadRequest("Color is invalid.");
        }

        // Try to find the category.
        var category = await _itemContext.Categories.FindAsync(id);
        if (category is null)
            // Return 404 if not found.
            return NotFound();

        // Edit the category and save to database.
        category.Color = request.Color;
        category.Name = request.Name;
        await _itemContext.SaveChangesAsync();

        // Return category as JSON.
        return Ok(category);
    }

    /// <summary>
    /// Reads a Category by id and returns it as JSON.
    /// </summary>
    /// <param name="id">Category Id.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadAsync(Guid id) =>
        await _itemContext.Categories.FindAsync(id) switch
        {
            Category category => Ok(category),
            _ => NotFound(),
        };

    /// <summary>
    /// Deletes a Category.
    /// </summary>
    /// <param name="id">Category Id.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // Find the category.
        var category = await _itemContext.Categories.FindAsync(id);
        if (category is null)
            // Return 404 if it doesn't exist.
            return NotFound();
        
        // Load the items within the category.
        await _itemContext.Entry(category).Collection(x => x.Items).LoadAsync();
        
        // Remove the category from the database.
        _itemContext.Categories.Remove(category);
        await _itemContext.SaveChangesAsync();

        // Return successful.
        return NoContent();
    }

    /// <summary>
    /// Reads all categories.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<Category>> GetAsync() =>
        await _itemContext.Categories.ToListAsync();
}
