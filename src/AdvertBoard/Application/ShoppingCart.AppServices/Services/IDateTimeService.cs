namespace AdvertBoard.AppServices.Services;

/// <summary>
/// Сервис предоставления даты и времени.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Возвращает текущую системную дату.
    /// </summary>
    /// <returns>Текущая системная дата.</returns>
    DateTime GetDateTime();
    
    /// <summary>
    /// Возвращает текущую системную дату в формате UTC.
    /// </summary>
    /// <returns>Текущая системная дата в формате UTC.</returns>
    DateTime GetUtcDateTime();
}