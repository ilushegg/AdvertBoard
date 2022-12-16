using System;

namespace AdvertBoard.Contracts;

/// <summary>
/// Модель ошибки.
/// </summary>
public class ResponseDto<T>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid ErrorStatus { get; set; }
    
    public string Message { get; set; }

    public T Response { get; set; }
    


}