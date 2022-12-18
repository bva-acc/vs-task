using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VSporte.Task.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerClubs_PlayerId",
                table: "PlayerClubs");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerClubs_PlayerId_ClubId",
                table: "PlayerClubs",
                columns: new[] { "PlayerId", "ClubId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_Name_Surname_Number",
                table: "Player",
                columns: new[] { "Name", "Surname", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_Name",
                table: "Clubs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ShortName",
                table: "Clubs",
                column: "ShortName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerClubs_PlayerId_ClubId",
                table: "PlayerClubs");

            migrationBuilder.DropIndex(
                name: "IX_Player_Name_Surname_Number",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_Name",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_ShortName",
                table: "Clubs");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerClubs_PlayerId",
                table: "PlayerClubs",
                column: "PlayerId");
        }
    }
}
