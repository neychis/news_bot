using Microsoft.EntityFrameworkCore;
using NewsAggregator.Models;
using NewsAggregator.Telegram.Interfaces;
using Telegram.Bot.Types;
using User = NewsAggregator.Models.User;

namespace NewsAggregator.Telegram;

public class TelegramBotHandler(
    TelegramService telegramService,
    NewsAggregatorContext dbContext
    ) : ITelegramBotHandler
{
    /// <inheritdoc />
    public async Task UpdateKeywordsAsync(Update update)
    {
        if (update.Message is not { } message || message.Text is null)
            return;

        var chatId = message.Chat.Id;
        var text = message.Text.Trim();

        if (text.StartsWith("/start"))
        {
            await telegramService.SendMessageAsync(chatId,
                "Привет! Отправь /setkeywords слово1, слово2, слово3, чтобы настроить подписку.");
            return;
        }

        if (text.StartsWith("/setkeywords"))
        {
            var words = text.Replace("/setkeywords", "").Trim()
                .Split(',')
                .Select(w => w.Trim().ToLower())
                .Where(w => !string.IsNullOrEmpty(w))
                .ToList();

            if (!words.Any())
            {
                await telegramService.SendMessageAsync(chatId,
                    "❌ Некорректный формат. Используй: `/setkeywords слово1, слово2, слово3`.");
                return;
            }

            var user = await dbContext.Users
                .Include(u => u.Preferences)
                .FirstOrDefaultAsync(u => u.ChatId == chatId);

            if (user == null)
            {
                user = new User { ChatId = chatId, Preferences = new List<Preference>() };
                dbContext.Users.Add(user);
            }
            else
            {
                user.Preferences.Clear();
            }
            
            user.Preferences ??= new List<Preference>();

            user.Preferences.AddRange(words.Select(w => new Preference { Keyword = w }));

            await dbContext.SaveChangesAsync();

            await telegramService.SendMessageAsync(chatId, "✅ Подписка обновлена! Теперь ты будешь получать новости по своим ключевым словам.");
        }
    }
}