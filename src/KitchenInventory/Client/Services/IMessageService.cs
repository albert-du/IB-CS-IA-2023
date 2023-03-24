namespace KitchenInventory.Client.Services;

/// <summary>
/// Message sending service.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Sends a message to the UI.
    /// </summary>
    /// <param name="level">Message level.</param>
    /// <param name="message">Text to display.</param>
    void Send(MessageLevel level, string message);

    /// <summary>
    /// Event for sent messages.
    /// </summary>
    event Action<MessageLevel, string> MessageReceived;
}
