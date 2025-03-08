namespace NewsAggregator.Telegram.Interfaces;

public interface ITelegramService
{
    /// <summary>
    /// Sends provided <paramref name="message" /> to the specific chat
    /// </summary>
    /// <param name="chatId">ID of a chat where to send the message</param>
    /// <param name="message">Message to send</param>
    Task SendMessageAsync(long chatId, string message);
}