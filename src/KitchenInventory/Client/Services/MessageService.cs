namespace KitchenInventory.Client.Services;

/// <inheritdoc/>
public class MessageService : IMessageService
{
    /// <inheritdoc/>
    public event Action<MessageLevel, string>? MessageReceived;

    /// <inheritdoc/>
    public void Send(MessageLevel level, string message)
    {
        Console.WriteLine($"[MESSAGE]::{message}");
        MessageReceived?.Invoke(level, message);
    }
}
