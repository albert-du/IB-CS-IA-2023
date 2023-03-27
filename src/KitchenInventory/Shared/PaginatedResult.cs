namespace KitchenInventory.Shared;

/// <summary>
/// Represents a generic Paginated list result from an API.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Page">Page number</param>
/// <param name="Count">On this page</param>
/// <param name="TotalResultCount">Total results.</param>
/// <param name="Result">List of items.</param>
public sealed record PaginatedResult<T>(int Page, int Count, int TotalResultCount, IReadOnlyList<T> Result)
{
    /// <summary>
    /// An empty paginated list.
    /// </summary>
    /// <returns></returns>
    public static PaginatedResult<T> Empty() =>
        new(1, 0, 0, Array.Empty<T>());

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalResultCount / (double)Count);
}
