using NewsAggregator.Models;

namespace NewsAggregator.Rss;

public class ScheduledRssJobs(IRssNewsService rssNewsService, NewsAggregatorContext dbContext): IScheduledRssJobs
{
    /// <inheritdoc />
    public async Task FetchAndStoreRssArticles()
    {
        IEnumerable<ArticleLog> articles = await rssNewsService.FetchArticlesAsync();
        await dbContext.ArticleLogs.AddRangeAsync(articles);
        await dbContext.SaveChangesAsync();
    }
}