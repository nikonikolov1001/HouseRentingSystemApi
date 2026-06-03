using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using HouseRentingSystemApi.Models.Enums;
using HouseRentingSystemApi.Services.Agents;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemApi.Services.Houses
{
    public class HouseService : IHouseService
    {
        private readonly AppDbContext context;
        private readonly IAgentService agentService;

        public HouseService(AppDbContext context, IAgentService agentService)
        {
            this.context = context;
            this.agentService = agentService;
        }

        public async Task<IndexViewModel> GetIndexAsync()
        {
            var totalHouses = await context.Houses.CountAsync();

            var houses = await ProjectHouses(context.Houses.AsNoTracking())
                .OrderByDescending(h => h.Id)
                .Take(3)
                .ToListAsync();

            return new IndexViewModel
            {
                TotalHouses = totalHouses,
                TotalRents = await context.Houses.CountAsync(h => h.RenterId != null),
                Houses = houses
            };
        }

        public async Task<AllHousesQueryModel> GetAllAsync(AllHousesQueryModel model)
        {
            if (model.CurrentPage < 1)
            {
                model.CurrentPage = 1;
            }

            var query = context.Houses.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.Category))
            {
                query = query.Where(h => h.Category.Name == model.Category);
            }

            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                var searchTerm = model.SearchTerm.ToLower();

                query = query.Where(h =>
                    h.Title.ToLower().Contains(searchTerm) ||
                    h.Address.ToLower().Contains(searchTerm) ||
                    h.Description.ToLower().Contains(searchTerm));
            }

            if (model.MinPrice.HasValue)
            {
                query = query.Where(h => h.PricePerMonth >= model.MinPrice.Value);
            }

            if (model.MaxPrice.HasValue)
            {
                query = query.Where(h => h.PricePerMonth <= model.MaxPrice.Value);
            }

            if (model.IsRented.HasValue)
            {
                query = query.Where(h => (h.RenterId != null) == model.IsRented.Value);
            }

            query = model.Sorting switch
            {
                HouseSorting.PriceAscending => query.OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => query.OrderBy(h => h.RenterId != null).ThenByDescending(h => h.Id),
                HouseSorting.Newest => query.OrderByDescending(h => h.Id),
                _ => query.OrderByDescending(h => h.Id)
            };

            var totalCount = await query.CountAsync();

            model.Houses = await ProjectHouses(query)
                .Skip((model.CurrentPage - 1) * AllHousesQueryModel.HousesPerPage)
                .Take(AllHousesQueryModel.HousesPerPage)
                .ToListAsync();

            model.TotalHousesCount = totalCount;
            model.Categories = await context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();

            return model;
        }

        public async Task<IEnumerable<HouseViewModel>> GetMineAsync(string userId)
            => await ProjectHouses(context.Houses.AsNoTracking().Where(h => h.Agent.UserId == userId))
                .OrderByDescending(h => h.Id)
                .ToListAsync();

        public async Task<HouseDetailsViewModel?> GetByIdAsync(int id)
            => await context.Houses
                .AsNoTracking()
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsViewModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    Description = h.Description,
                    PricePerMonth = h.PricePerMonth,
                    Category = h.Category.Name,
                    IsRented = h.RenterId != null,
                    OwnerName = h.Agent.User.UserName ?? string.Empty,
                    OwnerEmail = h.Agent.User.Email ?? string.Empty
                })
                .FirstOrDefaultAsync();

        public async Task<HouseDetailModel?> CreateAsync(HouseDetailModel model, string userId)
        {
            var agentId = await agentService.GetAgentIdByUserIdAsync(userId);
            if (agentId == null)
            {
                return null;
            }

            var categoryName = GetCategoryName(model.Category);

            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Name == categoryName);

            if (category == null)
            {
                category = new Category
                {
                    Name = categoryName
                };

                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }

            var newHouse = new House
            {
                Title = model.Title,
                Address = model.Address,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                PricePerMonth = model.PricePerMonth,
                CategoryId = category.Id,
                AgentId = agentId.Value
            };

            context.Houses.Add(newHouse);
            await context.SaveChangesAsync();

            return new HouseDetailModel
            {
                Id = newHouse.Id,
                Address = newHouse.Address,
                ImageUrl = newHouse.ImageUrl,
                Title = newHouse.Title,
                Description = newHouse.Description,
                PricePerMonth = newHouse.PricePerMonth,
                Category = model.Category
            };
        }

        private static IQueryable<HouseViewModel> ProjectHouses(IQueryable<House> houses)
            => houses.Select(h => new HouseViewModel
            {
                Id = h.Id,
                Title = h.Title,
                Address = h.Address,
                ImageUrl = h.ImageUrl,
                Description = h.Description,
                PricePerMonth = h.PricePerMonth,
                Category = h.Category.Name,
                IsRented = h.RenterId != null,
                OwnerName = h.Agent.User.UserName ?? string.Empty
            });

        private static string GetCategoryName(CategoryViewEnum category)
            => category switch
            {
                CategoryViewEnum.Cottage => "Cottage",
                CategoryViewEnum.SingleFamily => "Single-Family",
                CategoryViewEnum.Duplex => "Duplex",
                _ => "Cottage"
            };
    }
}
