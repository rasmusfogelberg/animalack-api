using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace animalackapi.Migrations
{
    public partial class EventsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetUser_Pets_PetsId",
                table: "PetUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PetUser_Users_UsersId",
                table: "PetUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetUser",
                table: "PetUser");

            migrationBuilder.RenameTable(
                name: "PetUser",
                newName: "UserPets");

            migrationBuilder.RenameIndex(
                name: "IX_PetUser_UsersId",
                table: "UserPets",
                newName: "IX_UserPets_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPets",
                table: "UserPets",
                columns: new[] { "PetsId", "UsersId" });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_PetId",
                table: "Event",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPets_Pets_PetsId",
                table: "UserPets",
                column: "PetsId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPets_Users_UsersId",
                table: "UserPets",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPets_Pets_PetsId",
                table: "UserPets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPets_Users_UsersId",
                table: "UserPets");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPets",
                table: "UserPets");

            migrationBuilder.RenameTable(
                name: "UserPets",
                newName: "PetUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserPets_UsersId",
                table: "PetUser",
                newName: "IX_PetUser_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetUser",
                table: "PetUser",
                columns: new[] { "PetsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PetUser_Pets_PetsId",
                table: "PetUser",
                column: "PetsId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetUser_Users_UsersId",
                table: "PetUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
