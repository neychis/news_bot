namespace NewsAggregator.Models;

/// <summary>
/// Class which specifies the user properties
/// </summary>
public class User
{
    /// <summary>
    /// ID of user
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// User name
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// ID of the chat where to send the messages to user
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    /// User preferences collection
    /// Navigation property
    /// </summary>
    public List<Preference>? Preferences { get; set; }
}