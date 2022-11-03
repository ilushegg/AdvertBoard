using Microsoft.EntityFrameworkCore;
using AdvertBoard.DataAccess;

namespace AdvertBoard.Migrations;

public class MigrationsDbContext : AdvertBoardContext
{
    public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options) : base(options)
    {
    }
}