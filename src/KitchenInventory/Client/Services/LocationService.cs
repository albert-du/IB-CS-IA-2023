namespace KitchenInventory.Client.Services;

/// <inheritdoc/>
public class LocationService : ILocationService
{
    private readonly IMessageService _messageService;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Creates a new instance of LocationService.
    /// </summary>
    /// <param name="messageService"></param>
    /// <param name="httpClient"></param>
    public LocationService(IMessageService messageService, HttpClient httpClient)
    {
        _messageService = messageService;
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Location>> AllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<IReadOnlyList<Location>>($"/api/location") ?? Array.Empty<Location>();
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
        return Array.Empty<Location>();
    }

    /// <inheritdoc/>
    public async Task<Location?> CreateAsync(LocationDTO request)
    {
        try
        {
            var resp = await _httpClient.PostAsJsonAsync("/api/location", request);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Location>();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while creating location: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while creating location: {ex.GetType()} {ex.Message}");

        }
        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var resp = await _httpClient.DeleteAsync($"/api/location/{id}");
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
                    _messageService.Send(MessageLevel.Error, $"Error while deleting location: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while deleting location: {ex.GetType()} {ex.Message}");
        }
        return false;
    }

    /// <inheritdoc/>
    public async Task<Location?> TryReadAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Location>($"/api/location/{id}");
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while retrieving location: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while retrieving location: {ex.GetType()} {ex.Message}");
        }
        return null;
    }

    /// <inheritdoc/>
    public async Task<Location?> UpdateAsync(Guid id, LocationDTO request)
    {
        try
        {
            var resp = await _httpClient.PutAsJsonAsync($"/api/location/{id}", request);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Location>();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageService.Send(MessageLevel.Error, "Not logged in.");
                    break;
                default:
                    _messageService.Send(MessageLevel.Error, $"Error while updating location: {ex.StatusCode} {ex.GetType()} {ex.Message}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _messageService.Send(MessageLevel.Error, $"Error while updating location: {ex.GetType()} {ex.Message}");
        }
        return null;
    }
}
