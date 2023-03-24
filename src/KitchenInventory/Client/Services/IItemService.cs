namespace KitchenInventory.Client.Services;

/// <summary>
/// Item Service for managing items.
/// </summary>
public interface IItemService
{
    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="request">Details for new item.</param>
    /// <returns>Item if successful, null otherwise.</returns>
    Task<Item?> CreateAsync(ItemDTO request);

    /// <summary>
    /// Reads an item by id.
    /// </summary>
    /// <param name="id">Id of the item to read.</param>
    /// <returns>Item if successful, null otherwise.</returns>
    Task<Item?> TryReadAsync(Guid id);

    /// <summary>
    /// Updates an item by id.
    /// </summary>
    /// <param name="id">Id of the item to update.</param>
    /// <param name="request">Details of the item to change.</param>
    /// <returns></returns>
    Task<Item?> UpdateAsync(Guid id, ItemDTO request);

    /// <summary>
    /// Deletes an item by id.
    /// </summary>
    /// <param name="id">Id of the item to delete.</param>
    /// <returns>Success of deletion.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Queries the database.
    /// </summary>
    /// <param name="page">Pagination page number.</param>
    /// <param name="count">Pagination count per page.</param>
    /// <param name="search">String to search for.</param>
    /// <param name="location">Location Id to restrict items to.</param>
    /// <param name="category">Category Id to restrict items to.</param>
    /// <returns>A paginated list of items.</returns>
    Task<PaginatedResult<Item>> SearchAsync(int page, int count, string? search = null, Guid? location = null, Guid? category = null);
    
    /// <summary>
    /// Retrieves all items from the database.
    /// </summary>
    /// <param name="page">Pagination page number.</param>
    /// <param name="count">Pagination count per page.</param>
    /// <returns></returns>
    Task<PaginatedResult<Item>> AllAsync(int page, int count);

    /// <summary>
    /// Retrieves the items soonest to expiration.
    /// </summary>
    /// <param name="page">Pagination page number.</param>
    /// <param name="count">Pagination count per page.</param>
    /// <returns></returns>
    Task<PaginatedResult<Item>> ExpiringAsync(int page, int count);
    
    /// <summary>
    /// Retrieves items that have been deleted.
    /// </summary>
    /// <param name="page">Pagination page number.</param>
    /// <param name="count">Pagination count per page.</param>
    /// <returns></returns>
    Task<PaginatedResult<Item>> HistoryAsync(int page, int count);
}
