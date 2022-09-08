using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDb.Infra.DataBase.Migrations
{
    public partial class ActorFilm_Many_To_Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Actors_ActorId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_ActorId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Films");

            migrationBuilder.CreateTable(
                name: "ActorFilm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorFilm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActorFilm_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorFilm_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorFilm_ActorId",
                table: "ActorFilm",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorFilm_FilmId",
                table: "ActorFilm",
                column: "FilmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorFilm");

            migrationBuilder.AddColumn<Guid>(
                name: "ActorId",
                table: "Films",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Films_ActorId",
                table: "Films",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Actors_ActorId",
                table: "Films",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
