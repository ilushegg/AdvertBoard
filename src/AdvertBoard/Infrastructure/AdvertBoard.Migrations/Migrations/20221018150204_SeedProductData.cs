using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class SeedProductData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
                $"INSERT INTO public.\"Category\"\r\n(\"Id\", \"Name\")\r\nVALUES('{Guid.NewGuid()}', 'Еда');\r\n";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
