using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardStorage.Data.Migrations
{
    public partial class Addownerlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cards",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                newName: "IX_Cards_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_OwnerId",
                table: "Cards",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_OwnerId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Cards",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_OwnerId",
                table: "Cards",
                newName: "IX_Cards_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
