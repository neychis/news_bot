using NewsAggregator.Models;
using Microsoft.EntityFrameworkCore;

namespace NewsAggregator;

public class NewsAggregatorContext(DbContextOptions<NewsAggregatorContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Preference> Preferences { get; set; }
    public DbSet<ArticleLog> ArticleLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Preferences)
            .WithOne(p => p.User);
    }
}