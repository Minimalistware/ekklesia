using Microsoft.EntityFrameworkCore.Migrations;

namespace ekklesia.Migrations
{
    public partial class ConvertionCult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cult_Convertions",
                table: "Occasions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cult_Convertions",
                table: "Occasions");
        }
    }
}
