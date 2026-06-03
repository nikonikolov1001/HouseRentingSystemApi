using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseRentingSystemApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgentsAndRentSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_UserId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Categories_CategoryId",
                table: "Houses");

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Houses",
                newName: "RenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Houses_UserId",
                table: "Houses",
                newName: "IX_Houses_RenterId");

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Houses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "f20d4125-4733-4299-a0b5-c367e5cbcd10", "guest@mail.com", true, false, null, "GUEST@MAIL.COM", "GUEST@MAIL.COM", "AQAAAAIAAYagAAAAEPUyaeqvXImUV5c4SNfpmLumrN780CEXD4AMblWCaWpnUyXcvTHsZorLzj9Muy9LNw==", null, false, "9f733b6a-3db3-4700-b314-c040bb4a54c6", false, "guest@mail.com" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "b194e9e4-0a4a-41ed-9e69-ab041c2f7d9d", "agent@mail.com", true, false, null, "AGENT@MAIL.COM", "AGENT@MAIL.COM", "AQAAAAIAAYagAAAAEGIU6poCItkR18s6pREU8/jX3WUzD5zJq6gJ8wnI3HRmytfVuGZc8rvSzls48WU+RA==", null, false, "86ad1efe-09cf-4b5b-b9e4-87f6bd27b38d", false, "agent@mail.com" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Cottage");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Single-Family");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Duplex");

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), "+359888888888", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.Sql("UPDATE [Houses] SET [AgentId] = '44a41a1c-943b-47e2-80e6-47463b6f139b' WHERE [AgentId] = '00000000-0000-0000-0000-000000000000'");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[,]
                {
                    { 1, "North London, UK (near the border)", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "Big House Marina" },
                    { 2, "Near the Sea Garden in Burgas, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" },
                    { 3, "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("44a41a1c-943b-47e2-80e6-47463b6f139b"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_AgentId",
                table: "Houses",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                table: "Agents",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Agents_AgentId",
                table: "Houses",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Categories_CategoryId",
                table: "Houses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Agents_AgentId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Categories_CategoryId",
                table: "Houses");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Houses_AgentId",
                table: "Houses");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Houses");

            migrationBuilder.RenameColumn(
                name: "RenterId",
                table: "Houses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Houses_RenterId",
                table: "Houses",
                newName: "IX_Houses_UserId");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Apartment");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Room");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "House");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "Title", "UserId" },
                values: new object[,]
                {
                    { 11, "ul. Vitosha 15, Sofia", 1, "Spacious modern apartment near city center with great view.", "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688", 850m, "Modern Apartment in Sofia Center", null },
                    { 12, "Studentski Grad 45, Sofia", 2, "Perfect for students, fully furnished studio.", "https://images.unsplash.com/photo-1493809842364-78817add7ffb", 450m, "Cozy Studio in Studentski Grad", null },
                    { 13, "Lozenets, Sofia", 1, "High-end penthouse with terrace and parking.", "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2", 2000m, "Luxury Penthouse", null },
                    { 14, "Bistritsa Village", 3, "Quiet place with yard and nature around.", "https://images.unsplash.com/photo-1572120360610-d971b9d7767c", 300m, "Small House in Village", null },
                    { 15, "Dragalevtsi, Sofia", 3, "Big house suitable for family with garden.", "https://images.unsplash.com/photo-1600585154340-be6161a56a0c", 1200m, "Family House with Garden", null },
                    { 16, "Mladost 2, Sofia", 1, "Comfortable one-bedroom apartment.", "https://images.unsplash.com/photo-1507089947368-19c1da9775ae", 600m, "One Bedroom Apartment", null },
                    { 17, "Nadezhda, Sofia", 2, "Budget room, ideal for short stay.", "https://images.unsplash.com/photo-1554995207-c18c203602cb", 200m, "Cheap Room for Rent", null },
                    { 18, "Varna Center", 1, "Beautiful apartment with sea view.", "https://images.unsplash.com/photo-1494526585095-c41746248156", 900m, "Sea View Apartment", null },
                    { 19, "Borovets", 3, "Wooden cabin perfect for winter getaway.", "https://images.unsplash.com/photo-1449844908441-8829872d2607", 700m, "Mountain Cabin", null },
                    { 20, "Plovdiv Center", 1, "Clean and minimalist design, great location.", "https://images.unsplash.com/photo-1499951360447-b19be8fe80f5", 650m, "Minimalist Apartment", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_UserId",
                table: "Houses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Categories_CategoryId",
                table: "Houses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
