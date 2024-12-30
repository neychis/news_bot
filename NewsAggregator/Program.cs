using Hangfire;
using Microsoft.EntityFrameworkCore;
using NewsAggregator;
using NewsAggregator.Rss;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NewsAggregatorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHttpClient();

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfireServer();
builder.Services.AddControllers();
builder.Services.AddScoped<IRssNewsService, RssNewsService>();
builder.Services.AddScoped<IScheduledRssJobs, ScheduledRssJobs>();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapHangfireDashboard("/hangfire"); 
}

app.Lifetime.ApplicationStarted.Register(() =>
{
    using var scope = app.Services.CreateScope();
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var scheduledRssJobs = scope.ServiceProvider.GetRequiredService<IScheduledRssJobs>();

    recurringJobManager.AddOrUpdate(
        "scheduledRssJobs",
        () => scheduledRssJobs.FetchAndStoreRssArticles(),
        Cron.Daily
    );
});


app.Run();