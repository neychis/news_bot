using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Models;

namespace NewsAggregator.RestApi;

[ApiController]
[Route("api/articlelog")]
public class ArticleLogController(NewsAggregatorContext dbContext): ControllerBase 
{
    /// <summary>
    /// Gets articles by certain <paramref name="keywords"/>
    /// </summary>
    /// <param name="keywords">A list of keywords</param>
    [HttpGet]
    public async Task<IActionResult> GetArticles(ICollection<string> keywords)
    {
        List<ArticleLog> articles = await dbContext.ArticleLogs
            .Where(article => keywords.Any(keyword => article.Keywords.Contains(keyword)))
            .ToListAsync();
        return Ok(articles);
    }
}