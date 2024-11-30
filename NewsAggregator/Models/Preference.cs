namespace NewsAggregator;

/// <summary>
/// User preferences
/// </summary>
public class Preference
{
    /// <summary>
    /// ID of preference
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Keyword for the news to filter by
    /// </summary>
    public string Keyword { get; set; }
    /// <summary>
    /// ID of user which has this preference
    /// </summary>
    public int UserId { get; set; }


    /// <summary>
    /// User which has this preference.
    /// Navigation property
    /// </summary>
    public User User { get; set; }
}