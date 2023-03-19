namespace KitchenInventory.Shared;

public record PaginatedResult<T>(int Page, int Count, int TotalResultCount, IReadOnlyList<T> Result);
