using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PearReview.Migrations
{
    /// <inheritdoc />
    public partial class Roles_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "233e6dbb-5925-4c23-a86c-18d501c53cde", "234d8db3-b25d-4b6e-bdec-96b944ae87f8", "Teacher", "TEACHER" },
                    { "4c5d0099-7b3a-44da-b6e8-d88959814aea", "a395e986-8ec5-44d0-80dd-2728bf7ec962", "Student", "STUDENT" },
                    { "902f24fa-879f-4263-875d-6f7599665c75", "11931df8-9549-4bd8-87db-63e4b943de08", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "233e6dbb-5925-4c23-a86c-18d501c53cde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c5d0099-7b3a-44da-b6e8-d88959814aea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "902f24fa-879f-4263-875d-6f7599665c75");
        }
    }
}
