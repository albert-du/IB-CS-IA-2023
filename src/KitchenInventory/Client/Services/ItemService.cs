namespace KitchenInventory.Client.Services;

using Microsoft.AspNetCore.WebUtilities;

/// <inheritdoc/>
public class ItemService : IItemService
{
    private readonly IMessageService _messageService;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Creates a new instance of ItemService.
    /// </summary>
    /// <param name="messageService">From DI container.</param>
    /// <param name="httpClient">From DI container.</param>
    public ItemService(IMessageService messageService, HttpClient httpClient)
    {
        _messageService = messageService;
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<PaginatedResult<Item>> AllAsync(int page, int count)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PaginatedResult<Item>>($"/api/item?page={page}&count={count}") ?? PaginatedResult<Item>.Empty();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.GetType()} {ex.Message}");
        }
        return PaginatedResult<Item>.Empty();
    }

    /// <inheritdoc/>
    public async Task<Item?> CreateAsync(ItemDTO request)
    {
        try
        {
            var resp = await _httpClient.PostAsJsonAsync("/api/item", request);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Item>();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while creating item: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while creating item: {ex.GetType()} {ex.Message}");
        }
        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var resp = await _httpClient.DeleteAsync($"/api/item/{id}");
            resp.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while deleting item: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while deleting item: {ex.GetType()} {ex.Message}");
        }
        return false;
    }

    /// <inheritdoc/>
    public async Task<PaginatedResult<Item>> ExpiringAsync(int page, int count)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PaginatedResult<Item>>($"/api/item/expiring?page={page}&count={count}") ?? PaginatedResult<Item>.Empty();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.GetType()} {ex.Message}");
        }
        return PaginatedResult<Item>.Empty();
    }

    /// <inheritdoc/>
    public async Task<PaginatedResult<Item>> HistoryAsync(int page, int count)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PaginatedResult<Item>>($"/api/item/history?page={page}&count={count}") ?? PaginatedResult<Item>.Empty();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.GetType()} {ex.Message}");
        }
        return PaginatedResult<Item>.Empty();
    }

    /// <inheritdoc/>
    public async Task<PaginatedResult<Item>> SearchAsync(int page, int count, string? search = null, Guid? location = null, Guid? category = null)
    {
        // Build query parameters.
        Dictionary<string, string> parameters = new()
        {
            [nameof(page)] = page.ToString(),
            [nameof(count)] = count.ToString()
        };

        if (location is Guid locationGuid)
            parameters.Add(nameof(location), locationGuid.ToString());

        if (category is Guid categoryGuid)
            parameters.Add(nameof(category), categoryGuid.ToString());

        if (!string.IsNullOrEmpty(search))
            parameters.Add("q", search);

        // Add parameters to url.
        var url = QueryHelpers.AddQueryString("/api/item/search", parameters);
        
        try
        {
            return await _httpClient.GetFromJsonAsync<PaginatedResult<Item>>(url) ?? PaginatedResult<Item>.Empty();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving items: {ex.GetType()} {ex.Message}");
        }
        return PaginatedResult<Item>.Empty();
    }

    /// <inheritdoc/>
    public async Task<Item?> TryReadAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Item>($"/api/item/{id}");
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving item: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving item: {ex.GetType()} {ex.Message}");
        }
        return null;
    }

    /// <inheritdoc/>
    public async Task<Item?> UpdateAsync(Guid id, ItemDTO request)
    {
        try
        {
            var resp = await _httpClient.PutAsJsonAsync($"/api/item/{id}", request);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Item>();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while updating item: {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch(Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while updating item: {ex.GetType()} {ex.Message}");
        }
        return null;
    }
}
