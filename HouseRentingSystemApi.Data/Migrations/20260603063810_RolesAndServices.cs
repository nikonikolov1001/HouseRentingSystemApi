using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseRentingSystemApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RolesAndServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17fa4b7c-1c3d-49ed-9506-5f81a43f2851", null, "Agent", "AGENT" },
                    { "8f48f1c4-c87f-4db9-9f02-9ca2a73e34ef", null, "User", "USER" },
                    { "ad5f9eb6-e84b-4ff5-9975-7df16f4e73f1", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d904c9a8-713b-44bf-8139-e3a933b16e21", "AQAAAAIAAYagAAAAEBSFTGlFioQuJAYHVa7CaMkG1WibxuKCI2K++vRu0MO8xFtyjgZgLEmI/yd25Yy9Eg==", "6b67e4ff-b05b-4f1c-90e6-995ebc1fb6a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22a9abc1-5804-412d-9b6d-c0d82ea39a26", "AQAAAAIAAYagAAAAEIGXmGQVQ+HEqlbsxDNYpnbtUBqnPXYIem6U6wi2Yi9S6KCo2otQt8PHDcTgP1vsdw==", "7d818c2e-8dd7-40de-92c5-df02a88ffe36" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8f48f1c4-c87f-4db9-9f02-9ca2a73e34ef", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { "17fa4b7c-1c3d-49ed-9506-5f81a43f2851", "dea12856-c198-4129-b3f3-b893d8395082" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad5f9eb6-e84b-4ff5-9975-7df16f4e73f1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8f48f1c4-c87f-4db9-9f02-9ca2a73e34ef", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "17fa4b7c-1c3d-49ed-9506-5f81a43f2851", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17fa4b7c-1c3d-49ed-9506-5f81a43f2851");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f48f1c4-c87f-4db9-9f02-9ca2a73e34ef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f20d4125-4733-4299-a0b5-c367e5cbcd10", "AQAAAAIAAYagAAAAEPUyaeqvXImUV5c4SNfpmLumrN780CEXD4AMblWCaWpnUyXcvTHsZorLzj9Muy9LNw==", "9f733b6a-3db3-4700-b314-c040bb4a54c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b194e9e4-0a4a-41ed-9e69-ab041c2f7d9d", "AQAAAAIAAYagAAAAEGIU6poCItkR18s6pREU8/jX3WUzD5zJq6gJ8wnI3HRmytfVuGZc8rvSzls48WU+RA==", "86ad1efe-09cf-4b5b-b9e4-87f6bd27b38d" });
        }
    }
}
