namespace AdvertBoard.AppServices.Services;

/// <inheritdoc />
public class DateTimeService : IDateTimeService
{
    /// <inheritdoc />
    public DateTime GetDateTime()
    {
        return DateTime.Now;
    }

    /// <inheritdoc />
    public DateTime GetUtcDateTime()
    {
        return DateTime.UtcNow;
    }
}