using Telegram.Bot.Types;

namespace NewsAggregator.Telegram.Interfaces;

public interface ITelegramBotHandler
{
    /// <summary>
    /// Updates keywords
    /// </summary>
    /// <param name="update"></param>
    Task UpdateKeywordsAsync(Update update);
}