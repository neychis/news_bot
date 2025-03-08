using System.ServiceModel.Syndication;
using NewsAggregator.Models;
using NewsAggregator.Rss.Interfaces;

namespace NewsAggregator.Rss;

/// <summary>
/// Service to get RSS news 
/// </summary>
public class RssNewsService(IHttpClientFactory httpClientFactory): IRssNewsService
{
    private const string RssFeedUrl = "https://elementy.ru/rss/news";

    /// <inheritdoc />
    public async Task<List<ArticleLog>> FetchArticlesAsync()
    {
        HttpClient client = httpClientFactory.CreateClient();
        HttpResponseMessage response = await client.GetAsync(RssFeedUrl);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var xmlReader = System.Xml.XmlReader.Create(stream);
        
        var today = DateTime.UtcNow.Date;

        var feed = SyndicationFeed.Load(xmlReader).Items.Where(item => item.PublishDate == today).ToList();
        List<ArticleLog> articles = [];

        if (feed.Count > 0)
        {
            articles.AddRange(feed.Select(CreateArticleLog));
        }

        return articles;
    }

    private static ArticleLog CreateArticleLog(SyndicationItem fetchedItem)
    {
        List<string> categoryList = fetchedItem.Categories.Select(c => c.Name).ToList();

        var categoriesString = string.Join(",", categoryList);

        var article = new ArticleLog
        {
            Title = fetchedItem.Title?.Text ?? string.Empty,
            Url = fetchedItem.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty,
            AccessedDt = fetchedItem.PublishDate.DateTime,
            Keywords = categoriesString,
            Summary = fetchedItem.Summary?.Text ?? string.Empty
        };
        return article;
    }
}