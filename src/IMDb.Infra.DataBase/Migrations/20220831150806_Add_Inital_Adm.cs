using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDb.Infra.DataBase.Migrations
{
    public partial class Add_Inital_Adm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Adms",
                columns: new[] { "Id", "Email", "Hash", "Name", "Salt" },
                values: new object[] { new Guid("026a029c-ca06-4094-8410-7a249ebbb340"), "adm@imdbserver.com", "2e8ab8b0879f61aff2efdf0b7303ff059daafb3a812167d19735534f83b21235", "Adm", "u/UxV85TWfn3DQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Adms",
                keyColumn: "Id",
                keyValue: new Guid("026a029c-ca06-4094-8410-7a249ebbb340"));
        }
    }
}
