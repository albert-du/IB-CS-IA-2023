namespace KitchenInventory.Client.Services;

/// <summary>
/// Location Service for managing locations.
/// </summary>
public interface ILocationService
{
    /// <summary>
    /// Creates a new location.
    /// </summary>
    /// <param name="request">Details for the new location.</param>
    /// <returns>The location, if successful, null otherwise.</returns>
    Task<Location?> CreateAsync(LocationDTO request);

    /// <summary>
    /// Read a location by id.
    /// </summary>
    /// <param name="id">The id of the location to read.</param>
    /// <returns>The location if successful, null otherwise.</returns>
    Task<Location?> TryReadAsync(Guid id);

    /// <summary>
    /// Update a location by id.
    /// </summary>
    /// <param name="id">The id of the location.</param>
    /// <param name="request">The details of the location.</param>
    /// <returns>The location, if successful, null otherwise.</returns>
    Task<Location?> UpdateAsync(Guid id, LocationDTO request);

    /// <summary>
    /// Delete an location by id.
    /// </summary>
    /// <param name="id">The id of the location to delete.</param>
    /// <returns>Success of deletion.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Reads all locations.
    /// </summary>
    /// <returns>All locations.</returns>
    Task<IReadOnlyList<Location>> AllAsync();
}
