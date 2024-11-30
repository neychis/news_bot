namespace NewsAggregator;

using Microsoft.EntityFrameworkCore;

public class NewsAggregatorContext(
    DbContextOptions<NewsAggregatorContext> options
) : DbContext(options)
{
    // Define DbSets for your entities
    public DbSet<User> Users { get; set; }
    public DbSet<Preference> Preferences { get; set; }
    public DbSet<ArticleLog> ArticleLogs { get; set; }

    // Pass options to the base DbContext constructor

    // Configure the DbContext options
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("DefaultConnection");
    }

    // Configure entity relationships or constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Preferences)
            .WithOne(p => p.User);

        // Additional configurations (if any)
    }
}