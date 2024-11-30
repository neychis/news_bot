namespace NewsAggregator;

/// <summary>
/// Log for acticles
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
    /// Url to article
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// Accessed date and time
    /// </summary>
    public DateTime AccessedDt { get; set; }
}