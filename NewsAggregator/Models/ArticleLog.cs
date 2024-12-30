namespace NewsAggregator.Models;

/// <summary>
/// Log for articles
/// </summary>
public class ArticleLog
{
    /// <summary>
    /// Article ID
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Article title
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Comma separated list of article's keywords
    /// </summary>
    public string Keywords { get; set; }
    /// <summary>
    /// The article summary
    /// </summary>
    public string Summary { get; set; }
    /// <summary>
    /// Url to article
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// Accessed date and time
    /// </summary>
    public DateTime AccessedDt { get; set; }
}