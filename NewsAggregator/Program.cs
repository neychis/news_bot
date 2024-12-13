using Microsoft.EntityFrameworkCore;
using NewsAggregator;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server configuration
builder.Services.AddDbContext<NewsAggregatorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline (if any)
app.Run();