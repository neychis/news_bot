using System.Text;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Models;
using NewsAggregator.Telegram.Interfaces;
using Telegram.Bot;

namespace NewsAggregator.Telegram;

public class ScheduledNewsDigest(NewsAggregatorContext context, ITelegramBotClient botClient): IScheduledNewsDigest
{
    /// <inheritdoc />
    public async Task SendDailyDigestAsync()
    {
        List<User> users = await context.Users
            .Include(u => u.Preferences)
            .ToListAsync();

        foreach (var user in users)
        {
            if (user.Preferences.Count == 0)
                continue;

            List<string> keywords = user.Preferences.Select(p => p.Keyword).ToList();

            List<ArticleLog> articles = await context.ArticleLogs
                .Where(a => keywords.Any(k => a.Keywords.Contains(k)))
                .OrderByDescending(a => a.AccessedDt)
                .Take(5)
                .ToListAsync();

            if (articles.Count == 0)
                continue; 

            var message = new StringBuilder();
            message.AppendLine("📰 *Ваш новостной дайджест:*");
            
            foreach (var article in articles)
            {
                message.AppendLine($"📌 [{article.Title}]({article.Url})");
            }

            await botClient.SendMessage(
                chatId: user.ChatId,
                text: message.ToString(),
                parseMode: global::Telegram.Bot.Types.Enums.ParseMode.Markdown
            );
        }
    }
}