using AdvertBoard.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AdvertBoard.DataAccess.Interfaces;

namespace AdvertBoard.DataAccess;

/// <summary>
/// Конфигурация контекста БД.
/// </summary>
public class AdvertBoardContextConfiguration : IDbContextOptionsConfigurator<AdvertBoardContext>
{
    private const string PostgesConnectionStringName = "PostgresAdvertBoardDb";
    private const string MsSqlConnectionStringName = "MsSqlAdvertBoardDb";
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AdvertBoardContextConfiguration"/>.
    /// </summary>
    /// <param name="configuration">Конфигурация.</param>
    /// <param name="loggerFactory">Фабрика средства логирования.</param>
    public AdvertBoardContextConfiguration(ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        _loggerFactory = loggerFactory;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Configure(DbContextOptionsBuilder<AdvertBoardContext> options)
    {
        string connectionString;

         var useMsSql = _configuration.GetSection("DataBaseOptions:UseMsSql").Get<bool>();

        if (!useMsSql)
        {
            connectionString = _configuration.GetConnectionString(PostgesConnectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    $"Не найдена строка подключения с именем '{PostgesConnectionStringName}'");
            }
            options.UseNpgsql(connectionString);
        }
        else
        {
            connectionString = _configuration.GetConnectionString(MsSqlConnectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    $"Не найдена строка подключения с именем '{MsSqlConnectionStringName}'");
            }
            options.UseSqlServer(connectionString);
        }
        
        options.UseLoggerFactory(_loggerFactory);
    }
}