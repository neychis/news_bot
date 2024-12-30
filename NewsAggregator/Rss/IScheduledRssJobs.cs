namespace NewsAggregator.Rss;

/// <summary>
/// interface for scheduled RSS jobs
/// </summary>
public interface IScheduledRssJobs
{
    /// <summary>
    /// Fetch and store in db rss articles
    /// </summary>
    Task FetchAndStoreRssArticles();
}