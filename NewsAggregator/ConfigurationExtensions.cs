namespace NewsAggregator;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Provides connection string for given <paramref name="configuration"/>
    /// </summary>
    /// <param name="configuration">Application configuration</param>
    public static string GetDatabaseConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("DefaultConnection")
               ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
               ?? throw new InvalidOperationException("Database connection string is not configured.");
    }
}