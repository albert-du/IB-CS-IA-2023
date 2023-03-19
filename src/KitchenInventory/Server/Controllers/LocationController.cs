namespace KitchenInventory.Server.Controllers;

using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controller for management of locations.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class LocationController : ControllerBase
{
    private readonly ItemContext _itemContext;

    /// <summary>
    /// Creates a new LocationController.
    /// </summary>
    /// <param name="itemContext">From DI container.</param>
    public LocationController(ItemContext itemContext)
    {
        _itemContext = itemContext;
    }

    /// <summary>
    /// Creates a new Location.
    /// </summary>
    /// <param name="request"></param>
    /// <response code="201">Location Created</response>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] LocationDTO request)
    {
        // Ensure that the color is valid.
        try
        {
            ColorTranslator.FromHtml(request.Color);
        }
        catch
        {
            return BadRequest("Color is invalid.");
        }

        // Create a new Location.
        var id = Guid.NewGuid();
        Location location = new()
        {
            Id = id,
            Color = request.Color,
            Name = request.Name
        };

        // Add the location to the database.
        await _itemContext.Locations.AddAsync(location);
        await _itemContext.SaveChangesAsync();

        // Return the location as JSON.
        return CreatedAtAction(nameof(ReadAsync), new { id }, location);
    }

    /// <summary>
    /// Modifies a Location.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] LocationDTO request)
    {
        // Ensure the color is valid.
        try
        {
            ColorTranslator.FromHtml(request.Color);
        }
        catch
        {
            return BadRequest("Color is invalid.");
        }

        // Find the Location.
        var location = await _itemContext.Locations.FindAsync(id);
        if (location is null)
            // 404 if it doesn't exist
            return NotFound();

        // Modify location.
        location.Color = request.Color;
        location.Name = request.Name;

        // Save location.
        await _itemContext.SaveChangesAsync();

        // Return location.
        return Ok(location);
    }

    /// <summary>
    /// Reads a Location by id and returns it as JSON.
    /// </summary>
    /// <param name="id">Location Id.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadAsync(Guid id) =>
        await _itemContext.Locations.FindAsync(id) switch
        {
            Location location => Ok(location),
            _ => NotFound(),
        };

    /// <summary>
    /// Deletes a Location.
    /// </summary>
    /// <param name="id">Location Id.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // Find the location.
        var location = await _itemContext.Locations.FindAsync(id);
        if (location is null)
            // 404 if the location doesn't exist.
            return NotFound();

        // Load the items.
        await _itemContext.Entry(location).Collection(x => x.Items).LoadAsync();
        
        // Remove the Location from the database.
        _itemContext.Locations.Remove(location);
        await _itemContext.SaveChangesAsync();

        // Return successful.
        return NoContent();
    }

    /// <summary>
    /// Reads all Locations.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<Location>> GetAsync() =>
        await _itemContext.Locations.ToListAsync();
}
