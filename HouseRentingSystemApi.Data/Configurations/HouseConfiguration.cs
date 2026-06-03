using HouseRentingSystemApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystemApi.Data.Configurations
{
    internal class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.Property(h => h.PricePerMonth)
                   .HasPrecision(18, 2);

            builder.HasData(SeedHouses());
        }

        private IEnumerable<House> SeedHouses()
        {
            return new List<House>
            {
                new House
                {
                    Id = 1,
                    Title = "Big House Marina",
                    Address = "North London, UK (near the border)",
                    Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                    ImageUrl = "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg",
                    PricePerMonth = 2100.00M,
                    CategoryId = 3,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b"),
                    RenterId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"
                },
                new House
                {
                    Id = 2,
                    Title = "Family House Comfort",
                    Address = "Near the Sea Garden in Burgas, Bulgaria",
                    Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                    ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                    PricePerMonth = 1200.00M,
                    CategoryId = 2,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                },
                new House
                {
                    Id = 3,
                    Title = "Grand House",
                    Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                    Description = "This luxurious house is everything you will need. It is just excellent.",
                    ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
                    PricePerMonth = 2000.00M,
                    CategoryId = 2,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                },
                new House
                {
                    Id = 4,
                    Title = "Sunny Cottage Retreat",
                    Address = "Rila Mountain Road 24, Samokov, Bulgaria",
                    Description = "A peaceful cottage with bright rooms, mountain views, a small garden, and enough space for weekend stays.",
                    ImageUrl = "https://images.unsplash.com/photo-1449844908441-8829872d2607",
                    PricePerMonth = 700.00M,
                    CategoryId = 1,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                },
                new House
                {
                    Id = 5,
                    Title = "Sea Garden Family Home",
                    Address = "Sea Garden District 18, Varna, Bulgaria",
                    Description = "A comfortable family property close to the sea garden with spacious bedrooms, parking, and a quiet balcony.",
                    ImageUrl = "https://images.unsplash.com/photo-1494526585095-c41746248156",
                    PricePerMonth = 1400.00M,
                    CategoryId = 2,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                },
                new House
                {
                    Id = 6,
                    Title = "Modern Duplex Plovdiv",
                    Address = "Kapana Quarter 42, Plovdiv, Bulgaria",
                    Description = "A modern duplex with open living area, two bedrooms, renovated kitchen, and a central city location.",
                    ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
                    PricePerMonth = 1100.00M,
                    CategoryId = 3,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                },
                new House
                {
                    Id = 7,
                    Title = "Quiet Cottage Garden",
                    Address = "Bistritsa Village Center 9, Sofia, Bulgaria",
                    Description = "A small cottage with a green yard, simple furniture, fresh air, and fast access to Sofia city routes.",
                    ImageUrl = "https://images.unsplash.com/photo-1572120360610-d971b9d7767c",
                    PricePerMonth = 650.00M,
                    CategoryId = 1,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                },
                new House
                {
                    Id = 8,
                    Title = "Family House Lozenets",
                    Address = "Lozenets Residential Area 31, Sofia, Bulgaria",
                    Description = "A well-kept single-family house near parks and public transport, suitable for long-term family renting.",
                    ImageUrl = "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2",
                    PricePerMonth = 1600.00M,
                    CategoryId = 2,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b"),
                    RenterId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"
                },
                new House
                {
                    Id = 9,
                    Title = "Duplex Near Business Park",
                    Address = "Mladost Business Park Street 12, Sofia, Bulgaria",
                    Description = "A clean duplex apartment near offices, metro station, shopping areas, and everyday city conveniences.",
                    ImageUrl = "https://images.unsplash.com/photo-1507089947368-19c1da9775ae",
                    PricePerMonth = 1250.00M,
                    CategoryId = 3,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                },
                new House
                {
                    Id = 10,
                    Title = "Cottage Lake Escape",
                    Address = "Pancharevo Lake Road 7, Sofia, Bulgaria",
                    Description = "A cozy cottage close to the lake with a fireplace, calm surroundings, and practical everyday equipment.",
                    ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688",
                    PricePerMonth = 900.00M,
                    CategoryId = 1,
                    AgentId = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b")
                }
            };
        }
    }
}
