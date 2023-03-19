namespace KitchenInventory.Server;

using Microsoft.EntityFrameworkCore;

internal static class Utilities
{
    internal static async Task<PaginatedResult<T>> PaginateAsync<T>(this IQueryable<T> source, int page, int count)
    {
        var content =
            await source.Skip((page - 1) * count) // Skip pages.
                        .Take(count)              // Take items.
                        .ToListAsync();

        var length = await source.CountAsync();
        return new(page, count, length, content);
    }
}
