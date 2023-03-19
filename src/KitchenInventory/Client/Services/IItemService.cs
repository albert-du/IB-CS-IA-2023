namespace KitchenInventory.Client.Services;

public interface IItemService
{
    Task<Item> CreateAsync(ItemDTO request);
    Task<Item?> TryReadAsync(Guid id);
    Task UpdateAsync(Guid id, ItemDTO request);
    Task DeleteAsync(Guid id);
    Task<PaginatedResult<Item>> SearchAsync(string search, int page, int count, Guid? location = null, Guid? category = null);
    Task<PaginatedResult<Item>> AllAsync(int page, int count);
    Task<PaginatedResult<Item>> ExpiringAsync(int page, int count);
    Task<PaginatedResult<Item>> HistoryAsync(int page, int count);
}
