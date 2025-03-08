namespace NewsAggregator.Rss.Interfaces;

/// <summary>
/// interface for scheduled RSS jobs
/// </summary>
public interface IScheduledRssJobs
{
    /// <summary>
    /// Fetch and store in db rss articles
    /// </summary>
    Task FetchAndStoreRssArticles();

    /// <summary>
    /// Cleans up old articles in the database
    /// </summary>
    void CleanOldArticles();
}