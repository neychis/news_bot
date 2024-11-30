using Microsoft.EntityFrameworkCore;
using NewsAggregator;

var builder = WebApplication.CreateBuilder(args);

// Add the DbContext to the DI container
builder.Services.AddDbContext<NewsAggregatorContext>(options =>
    options.UseSqlServer("DefaultConnection"));

var app = builder.Build();

app.Run();