namespace NewsAggregator.Telegram.Interfaces;

public interface IScheduledNewsDigest
{
    /// <summary>
    /// Sends daily news digest
    /// </summary>
    Task SendDailyDigestAsync();
}