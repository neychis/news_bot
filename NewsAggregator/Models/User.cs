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
    public string Name { get; set; }
    /// <summary>
    /// User e-mail
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User preferences collection
    /// Navigation property
    /// </summary>
    public ICollection<Preference> Preferences { get; set; }
}