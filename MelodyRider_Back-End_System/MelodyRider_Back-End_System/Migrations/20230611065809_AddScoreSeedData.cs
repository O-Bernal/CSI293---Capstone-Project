using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MelodyRider_Back_End_System.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7557a323-4d01-45f2-a979-8f4a8652b18a", "49d60260-6adb-4a60-90bc-981aaaaa1e96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c3ca881e-db63-4540-bf77-56b7525ff44c", "f753950d-7d8c-43c0-996d-96f4b47e809e" });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "ScoreId", "GameId", "ScoreValue", "UserId" },
                values: new object[,]
                {
                    { 2, 1, 900, "2" },
                    { 3, 1, 800, "2" },
                    { 4, 1, 700, "2" },
                    { 5, 1, 600, "2" }
                });

            migrationBuilder.UpdateData(
                table: "UserAchievements",
                keyColumns: new[] { "AchievementId", "UserId" },
                keyValues: new object[] { 1, "2" },
                column: "DateEarned",
                value: new DateTime(2023, 6, 10, 23, 58, 9, 566, DateTimeKind.Local).AddTicks(3517));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "26cc0ad5-8eb8-43ac-a2bd-05525d9d9b63", "4f6f8a47-7a62-4121-8586-6167c293c9ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4fd3ca63-b210-44cd-ac3d-b8871baa4b27", "dbc52a1f-00a7-42e0-b51d-93435b530fc6" });

            migrationBuilder.UpdateData(
                table: "UserAchievements",
                keyColumns: new[] { "AchievementId", "UserId" },
                keyValues: new object[] { 1, "2" },
                column: "DateEarned",
                value: new DateTime(2023, 6, 1, 13, 45, 16, 747, DateTimeKind.Local).AddTicks(7562));
        }
    }
}
