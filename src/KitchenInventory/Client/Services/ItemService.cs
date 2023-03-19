using System.Net.Http.Json;
using System.Net.Http;
using System.Net;

namespace KitchenInventory.Client.Services;

public class ItemService : IItemService
{
    private readonly IMessageService _messageService;
    private readonly HttpClient _httpClient;

    public ItemService(IMessageService messageService, HttpClient httpClient)
    {
        _messageService = messageService;
        _httpClient = httpClient;
    }

    public async Task<PaginatedResult<Item>> AllAsync(int page, int count)
    {
        throw new NotImplementedException();
    }

    public Task<Item> CreateAsync(ItemDTO request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<Item>> ExpiringAsync(int page, int count)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<Item>> HistoryAsync(int page, int count)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<Item>> SearchAsync(string search, int page, int count, Guid? location = null, Guid? category = null)
    {
        throw new NotImplementedException();
    }

    public Task<Item?> TryReadAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, ItemDTO request)
    {
        throw new NotImplementedException();
    }
}
