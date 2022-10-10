using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDb.Infra.DataBase.Migrations
{
    public partial class Add_IsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Adms",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Adms");
        }
    }
}
