using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.EntityFrameworkCore;
using NewsAggregator;
using NewsAggregator.Rss.Interfaces;
using NewsAggregator.Telegram.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5001");

builder.Services.AddApplicationServices();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[]
        {
            new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
            {
                SslRedirect = false,
                RequireSsl = false,
                LoginCaseSensitive = true,
                Users = new[]
                {
                    new BasicAuthAuthorizationUser
                    {
                        Login = "admin",
                        PasswordClear = "admin"
                    }
                }
            })
        }
    });
    app.MapHangfireDashboard();
}

app.Lifetime.ApplicationStarted.Register(() =>
{
    using var scope = app.Services.CreateScope();
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var scheduledRssJobs = scope.ServiceProvider.GetRequiredService<IScheduledRssJobs>();
    
    recurringJobManager.AddOrUpdate(
        "cleanup-old-articles",
        () => scheduledRssJobs.CleanOldArticles(),
        "0 3 * * *"
    );

    recurringJobManager.AddOrUpdate(
        "fetch-today-rss-articles",
        () => scheduledRssJobs.FetchAndStoreRssArticles(),
        "7 16 * * *"
    );
});


app.Run();