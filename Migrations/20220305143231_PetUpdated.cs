using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace animalackapi.Migrations
{
    public partial class PetUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetUser_Pets_PetsPetId",
                table: "PetUser");

            migrationBuilder.RenameColumn(
                name: "PetsPetId",
                table: "PetUser",
                newName: "PetsId");

            migrationBuilder.RenameColumn(
                name: "PetId",
                table: "Pets",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetUser_Pets_PetsId",
                table: "PetUser",
                column: "PetsId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetUser_Pets_PetsId",
                table: "PetUser");

            migrationBuilder.RenameColumn(
                name: "PetsId",
                table: "PetUser",
                newName: "PetsPetId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pets",
                newName: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetUser_Pets_PetsPetId",
                table: "PetUser",
                column: "PetsPetId",
                principalTable: "Pets",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
