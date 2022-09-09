using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDb.Infra.DataBase.Migrations
{
    public partial class Director_And_Gender_MTM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorFilm_Actors_ActorId",
                table: "ActorFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorFilm_Films_FilmId",
                table: "ActorFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmImage_Films_FilmId",
                table: "FilmImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Films_Directors_DirectorId",
                table: "Films");

            migrationBuilder.DropForeignKey(
                name: "FK_Films_Genders_GenderId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_DirectorId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_GenderId",
                table: "Films");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmImage",
                table: "FilmImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorFilm",
                table: "ActorFilm");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Films");

            migrationBuilder.RenameTable(
                name: "FilmImage",
                newName: "FilmImages");

            migrationBuilder.RenameTable(
                name: "ActorFilm",
                newName: "ActorFilms");

            migrationBuilder.RenameIndex(
                name: "IX_FilmImage_FilmId",
                table: "FilmImages",
                newName: "IX_FilmImages_FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorFilm_FilmId",
                table: "ActorFilms",
                newName: "IX_ActorFilms_FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorFilm_ActorId",
                table: "ActorFilms",
                newName: "IX_ActorFilms_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmImages",
                table: "FilmImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorFilms",
                table: "ActorFilms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DirectorFilms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorFilms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectorFilms_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorFilms_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenderFilms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderFilms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenderFilms_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenderFilms_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectorFilms_DirectorId",
                table: "DirectorFilms",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorFilms_FilmId",
                table: "DirectorFilms",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderFilms_FilmId",
                table: "GenderFilms",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderFilms_GenderId",
                table: "GenderFilms",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorFilms_Actors_ActorId",
                table: "ActorFilms",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorFilms_Films_FilmId",
                table: "ActorFilms",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmImages_Films_FilmId",
                table: "FilmImages",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorFilms_Actors_ActorId",
                table: "ActorFilms");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorFilms_Films_FilmId",
                table: "ActorFilms");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmImages_Films_FilmId",
                table: "FilmImages");

            migrationBuilder.DropTable(
                name: "DirectorFilms");

            migrationBuilder.DropTable(
                name: "GenderFilms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmImages",
                table: "FilmImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorFilms",
                table: "ActorFilms");

            migrationBuilder.RenameTable(
                name: "FilmImages",
                newName: "FilmImage");

            migrationBuilder.RenameTable(
                name: "ActorFilms",
                newName: "ActorFilm");

            migrationBuilder.RenameIndex(
                name: "IX_FilmImages_FilmId",
                table: "FilmImage",
                newName: "IX_FilmImage_FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorFilms_FilmId",
                table: "ActorFilm",
                newName: "IX_ActorFilm_FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorFilms_ActorId",
                table: "ActorFilm",
                newName: "IX_ActorFilm_ActorId");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectorId",
                table: "Films",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "Films",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmImage",
                table: "FilmImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorFilm",
                table: "ActorFilm",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Films_DirectorId",
                table: "Films",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_GenderId",
                table: "Films",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorFilm_Actors_ActorId",
                table: "ActorFilm",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorFilm_Films_FilmId",
                table: "ActorFilm",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmImage_Films_FilmId",
                table: "FilmImage",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Directors_DirectorId",
                table: "Films",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Genders_GenderId",
                table: "Films",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
