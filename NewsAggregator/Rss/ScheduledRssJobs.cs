using NewsAggregator.Models;
using NewsAggregator.Rss.Interfaces;

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
    
    /// <inheritdoc />
    public void CleanOldArticles()
    {
        var today = DateTime.UtcNow.Date;
        List<ArticleLog> oldArticles = dbContext.ArticleLogs.Where(a => a.AccessedDt < today).ToList();

        if (oldArticles.Count != 0)
        {
            dbContext.ArticleLogs.RemoveRange(oldArticles);
            dbContext.SaveChanges();
        }
    }
}