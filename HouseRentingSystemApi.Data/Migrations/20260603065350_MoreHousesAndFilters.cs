using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseRentingSystemApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreHousesAndFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf4f0ac0-2e1b-4edc-b85e-73aabfdef70c", "AQAAAAIAAYagAAAAEHJrQUO0lMvHP8Y4TbdOgD2p6HoaEQsZJZ/G7+PZ+xGzWlSoIYCvWgjD+OenNQt1Og==", "ba5cd890-b6c7-486b-b651-040cd1b2ea7a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "287d1470-b5d8-4a81-92c3-73d73ec4bc1b", "AQAAAAIAAYagAAAAELB0CBtYCWmxyHYl11iC/mM1Vgn9u2y79bmxgtMyT8aNIn7CWIbCs0k+w414OZp2eA==", "76112f18-e640-40ef-8a89-efd7c8ddd4c3" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[,]
                {
                    { 4, "Rila Mountain Road 24, Samokov, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 1, "A peaceful cottage with bright rooms, mountain views, a small garden, and enough space for weekend stays.", "https://images.unsplash.com/photo-1449844908441-8829872d2607", 700.00m, null, "Sunny Cottage Retreat" },
                    { 5, "Sea Garden District 18, Varna, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 2, "A comfortable family property close to the sea garden with spacious bedrooms, parking, and a quiet balcony.", "https://images.unsplash.com/photo-1494526585095-c41746248156", 1400.00m, null, "Sea Garden Family Home" },
                    { 6, "Kapana Quarter 42, Plovdiv, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 3, "A modern duplex with open living area, two bedrooms, renovated kitchen, and a central city location.", "https://images.unsplash.com/photo-1600585154340-be6161a56a0c", 1100.00m, null, "Modern Duplex Plovdiv" },
                    { 7, "Bistritsa Village Center 9, Sofia, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 1, "A small cottage with a green yard, simple furniture, fresh air, and fast access to Sofia city routes.", "https://images.unsplash.com/photo-1572120360610-d971b9d7767c", 650.00m, null, "Quiet Cottage Garden" },
                    { 8, "Lozenets Residential Area 31, Sofia, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 2, "A well-kept single-family house near parks and public transport, suitable for long-term family renting.", "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2", 1600.00m, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "Family House Lozenets" },
                    { 9, "Mladost Business Park Street 12, Sofia, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 3, "A clean duplex apartment near offices, metro station, shopping areas, and everyday city conveniences.", "https://images.unsplash.com/photo-1507089947368-19c1da9775ae", 1250.00m, null, "Duplex Near Business Park" },
                    { 10, "Pancharevo Lake Road 7, Sofia, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 1, "A cozy cottage close to the lake with a fireplace, calm surroundings, and practical everyday equipment.", "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688", 900.00m, null, "Cottage Lake Escape" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 10);

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
        }
    }
}
