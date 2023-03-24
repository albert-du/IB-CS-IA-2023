namespace KitchenInventory.Client.Services;

/// <summary>
/// Category REST API Client.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly IMessageService _messageService;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Creates a new instance of a CategoryService.
    /// </summary>
    /// <param name="messageService">From DI container.</param>
    /// <param name="httpClient">From DI container.</param>
    public CategoryService(IMessageService messageService, HttpClient httpClient)
    {
        _messageService = messageService;
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Category>> AllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<IReadOnlyList<Category>>($"/api/category") ?? Array.Empty<Category>();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving categories: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving categories: {ex.GetType()} {ex.Message}");
        }
        return Array.Empty<Category>();
    }

    /// <inheritdoc/>
    public async Task<Category?> CreateAsync(CategoryDTO request)
    {
        try
        {
            var resp = await _httpClient.PostAsJsonAsync("/api/category", request);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Category>();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while creating category: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while creating category: {ex.GetType()} {ex.Message}");
        }
        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var resp = await _httpClient.DeleteAsync($"/api/category/{id}");
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
                    _messageService.Send(MessageLevel.Error, $"Error while deleting category: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while deleting category: {ex.GetType()} {ex.Message}");
        }
        return false;
    }

    /// <inheritdoc/>
    public async Task<Category?> TryReadAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Category>($"/api/category/{id}");
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving category: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving category: {ex.GetType()} {ex.Message}");
        }
        return null;
    }

    /// <inheritdoc/>
    public async Task<Category?> UpdateAsync(Guid id, CategoryDTO request)
    {
        try
        {
            var resp = await _httpClient.PutAsJsonAsync($"/api/category/{id}", request);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Category>();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while updating category: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving category: {ex.GetType()} {ex.Message}");
        }
        return null;
    }
}
