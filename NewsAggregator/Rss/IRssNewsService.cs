using NewsAggregator.Models;

namespace NewsAggregator.Rss;

public interface IRssNewsService
{
    /// <summary>
    /// Asynchronously fetches articles
    /// </summary>
    Task<List<ArticleLog>> FetchArticlesAsync();
}