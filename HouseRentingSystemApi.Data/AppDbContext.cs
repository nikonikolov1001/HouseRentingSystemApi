using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Data.DataConstants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HouseRentingSystemApi.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<House> Houses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<House>()
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<House>()
                .HasOne(h => h.Agent)
                .WithMany(a => a.ManagedHouses)
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<House>()
                .HasOne(h => h.Renter)
                .WithMany()
                .HasForeignKey(h => h.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Agent>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Agent>(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUsers(builder);
            SeedRoles(builder);
            SeedUserRoles(builder);
            SeedAgent(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var agentUser = new ApplicationUser
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "AGENT@MAIL.COM",
                Email = "agent@mail.com",
                NormalizedEmail = "AGENT@MAIL.COM",
                EmailConfirmed = true
            };

            agentUser.PasswordHash = hasher.HashPassword(agentUser, "agent123");

            var guestUser = new ApplicationUser
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "GUEST@MAIL.COM",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM",
                EmailConfirmed = true
            };

            guestUser.PasswordHash = hasher.HashPassword(guestUser, "guest123");

            builder.Entity<ApplicationUser>().HasData(agentUser, guestUser);
        }

        private static void SeedAgent(ModelBuilder builder)
        {
            builder.Entity<Agent>().HasData(new Agent
            {
                Id = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b"),
                PhoneNumber = "+359888888888",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            });
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = AppRoles.UserRoleId,
                    Name = AppRoles.User,
                    NormalizedName = AppRoles.User.ToUpperInvariant()
                },
                new IdentityRole
                {
                    Id = AppRoles.AgentRoleId,
                    Name = AppRoles.Agent,
                    NormalizedName = AppRoles.Agent.ToUpperInvariant()
                },
                new IdentityRole
                {
                    Id = AppRoles.AdminRoleId,
                    Name = AppRoles.Admin,
                    NormalizedName = AppRoles.Admin.ToUpperInvariant()
                });
        }

        private static void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    RoleId = AppRoles.AgentRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    RoleId = AppRoles.UserRoleId
                });
        }
    }
}
