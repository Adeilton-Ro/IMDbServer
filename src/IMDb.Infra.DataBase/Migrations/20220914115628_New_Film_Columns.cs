using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDb.Infra.DataBase.Migrations
{
    public partial class New_Film_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Voters",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Synopsis",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Voters",
                table: "Films");
        }
    }
}
