using Hangfire;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Rss;
using NewsAggregator.Rss.Interfaces;
using NewsAggregator.Telegram;
using NewsAggregator.Telegram.Interfaces;
using Telegram.Bot;

namespace NewsAggregator;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<NewsAggregatorContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));
        
        var token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN") 
                    ?? throw new InvalidOperationException("TELEGRAM_BOT_TOKEN is not set");

        services.AddHttpClient<ITelegramBotClient, TelegramBotClient>(client =>
            new TelegramBotClient(token));

        services.AddHangfire(config => 
            config.UseSqlServerStorage(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));

        services.AddHangfireServer();

        services.AddControllers();

        services.AddScoped<IRssNewsService, RssNewsService>();
        services.AddScoped<IScheduledRssJobs, ScheduledRssJobs>();
        // services.AddScoped<ITelegramService, TelegramService>();
        // services.AddScoped<IScheduledNewsDigest, ScheduledNewsDigest>();
        // services.AddScoped<ITelegramBotHandler, TelegramBotHandler>();

        return services;
    }
}