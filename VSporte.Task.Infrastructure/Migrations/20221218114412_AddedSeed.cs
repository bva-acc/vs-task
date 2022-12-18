using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VSporte.Task.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Спартак", "Спартак" },
                    { 2, "Крылья Советов", "КС" }
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "Name", "Number", "Patronymic", "Surname" },
                values: new object[,]
                {
                    { 1, "Иван", 11, "Иванович", "Иванов" },
                    { 2, "Петр", 22, "Петрович", "Петров" },
                    { 3, "Сидор", 33, "Сидорович", "Сидоров" }
                });

            migrationBuilder.InsertData(
                table: "GameEvents",
                columns: new[] { "Id", "ClubId", "MomentTime", "PlayerId", "Type" },
                values: new object[,]
                {
                    { 1, 1, "2", 1, "Забил мяч в свои ворота" },
                    { 2, 2, "45+2", 2, "Получил желтую карточку" }
                });

            migrationBuilder.InsertData(
                table: "PlayerClubs",
                columns: new[] { "Id", "ClubId", "PlayerId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameEvents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameEvents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlayerClubs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlayerClubs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlayerClubs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
