using NewsAggregator.Telegram.Interfaces;
using Telegram.Bot;

namespace NewsAggregator.Telegram;

public class TelegramService: ITelegramService
{
    private readonly ITelegramBotClient _botClient;

    public TelegramService(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    /// <inheritdoc />
    public async Task SendMessageAsync(long chatId, string message)
    {
        await _botClient.SendTextMessageAsync(chatId, message);
    }
}