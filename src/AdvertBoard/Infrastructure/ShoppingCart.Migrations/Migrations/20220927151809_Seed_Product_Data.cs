using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Migrations.Migrations
{
    public partial class Seed_Product_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
                $"INSERT INTO public.\"Products\" (\"Id\", \"Name\", \"Description\", \"Price\") VALUES('{Guid.NewGuid()}', 'Телевизор', 'Описание телевизора', 500)";
            
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
