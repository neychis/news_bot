namespace NewsAggregator;

using Microsoft.EntityFrameworkCore;

public class NewsAggregatorContext : DbContext
{
    public NewsAggregatorContext(DbContextOptions<NewsAggregatorContext> options) : base(options)
    {
    }

    // Define DbSets for your entities
    public DbSet<User> Users { get; set; }
    public DbSet<Preference> Preferences { get; set; }
    public DbSet<ArticleLog> ArticleLogs { get; set; }

    // Configure entity relationships or constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Preferences)
            .WithOne(p => p.User);

        // Additional configurations (if any)
    }
}