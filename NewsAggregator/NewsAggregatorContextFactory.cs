using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NewsAggregator
{
    public class NewsAggregatorContextFactory : IDesignTimeDbContextFactory<NewsAggregatorContext>
    {
        public NewsAggregatorContext CreateDbContext(string[] args)
        {
            // Build options for design-time (EF CLI) usage
            var builder = new DbContextOptionsBuilder<NewsAggregatorContext>();
            
            // Provide the connection string explicitly or pull from environment/args
            builder.UseSqlServer("Server=localhost,1433;Database=newsAggregator_database;User Id=sa;Password=StrongPassword123!;TrustServerCertificate=True;");
            
            return new NewsAggregatorContext(builder.Options);
        }
    }
}