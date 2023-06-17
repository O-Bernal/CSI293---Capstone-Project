using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelodyRider_Back_End_System.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "SecurityStamp" },
                values: new object[] { "524ae749-0327-4fec-8675-2d085c49e2a8", false, "852f1f9f-0160-4ae2-8d17-2d06e9850b5b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "SecurityStamp" },
                values: new object[] { "4c519960-9412-4819-b2e3-a011e5abeebe", false, "7a1ec39b-bee2-481e-a084-fcedfa38ca78" });

            migrationBuilder.UpdateData(
                table: "UserAchievements",
                keyColumns: new[] { "AchievementId", "UserId" },
                keyValues: new object[] { 1, "2" },
                column: "DateEarned",
                value: new DateTime(2023, 6, 16, 14, 4, 11, 101, DateTimeKind.Local).AddTicks(531));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

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

            migrationBuilder.UpdateData(
                table: "UserAchievements",
                keyColumns: new[] { "AchievementId", "UserId" },
                keyValues: new object[] { 1, "2" },
                column: "DateEarned",
                value: new DateTime(2023, 6, 10, 23, 58, 9, 566, DateTimeKind.Local).AddTicks(3517));
        }
    }
}
