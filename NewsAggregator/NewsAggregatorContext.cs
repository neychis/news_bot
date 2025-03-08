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
        // Конфигурация User и Preference
        modelBuilder.Entity<User>()
            .HasMany(u => u.Preferences)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId) // Явный внешний ключ
            .OnDelete(DeleteBehavior.Cascade); // Удаление преференсов при удалении юзера

        // Конфигурация ArticleLog
        modelBuilder.Entity<ArticleLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(4000);
            entity.Property(e => e.Summary).HasMaxLength(4000);
            entity.Property(e => e.Keywords).HasMaxLength(4000);
            entity.Property(e => e.Url).HasMaxLength(4000);
            entity.Property(e => e.AccessedDt).HasColumnType("datetime2");

            // Добавляем уникальность на URL, если важно избежать дубликатов
            entity.HasIndex(e => e.Url).IsUnique();
        });
    }
}
