namespace KitchenInventory.Client.Services;

public interface IMessageService
{
    void Send(MessageLevel level, string message);
    event Action<MessageLevel, string> MessageReceived;
}
