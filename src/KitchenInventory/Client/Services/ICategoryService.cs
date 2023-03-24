namespace KitchenInventory.Client.Services;

/// <summary>
/// Category Service for managing categories.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Create a new category.
    /// </summary>
    /// <param name="request">Details of the new category.</param>
    /// <returns>Newly created category if successful.</returns>
    Task<Category?> CreateAsync(CategoryDTO request);

    /// <summary>
    /// Reads a category.
    /// </summary>
    /// <param name="id">Id of the category to read.</param>
    /// <returns>Category if it exists, null otherwise.</returns>
    Task<Category?> TryReadAsync(Guid id);

    /// <summary>
    /// Updates a category.
    /// </summary>
    /// <param name="id">The id of the category to modify.</param>
    /// <param name="request">The details to set the category to.</param>
    /// <returns>The category if successful, null otherwise.</returns>
    Task<Category?> UpdateAsync(Guid id, CategoryDTO request);

    /// <summary>
    /// Deletes an category.
    /// </summary>
    /// <param name="id">Id of the category to delete.</param>
    /// <returns>Success of deletion.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Reads all categories.
    /// </summary>
    /// <returns>A list of categories.</returns>
    Task<IReadOnlyList<Category>> AllAsync();
}
