namespace KitchenInventory.Client.Services;

public class MessageService : IMessageService
{
    public event Action<MessageLevel, string>? MessageReceived;

    public void Send(MessageLevel level, string message) =>
        MessageReceived?.Invoke(level, message);
}
