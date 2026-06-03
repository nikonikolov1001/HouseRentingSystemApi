using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using HouseRentingSystemApi.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HouseRentingSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HouseController : ControllerBase
    {
        private readonly AppDbContext context;

        public HouseController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("Index")]
        [Produces(typeof(IndexViewModel))]
        public async Task<IActionResult> GetIndex()
        {
            var totalHouses = await context.Houses.CountAsync();

            var houses = await context.Houses
                .AsNoTracking()
                .Include(h => h.Category)
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .OrderByDescending(h => h.Id)
                .Take(3)
                .Select(h => new HouseViewModel
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
                })
                .ToListAsync();

            var model = new IndexViewModel
            {
                TotalHouses = totalHouses,
                TotalRents = await context.Houses.CountAsync(h => h.RenterId != null),
                Houses = houses
            };

            return Ok(model);
        }

        [HttpGet("All")]
        [Produces(typeof(AllHousesQueryModel))]
        public async Task<IActionResult> GetAll([FromQuery] AllHousesQueryModel model)
        {
            if (model.CurrentPage < 1)
            {
                model.CurrentPage = 1;
            }

            var query = context.Houses
                .AsNoTracking()
                .Include(h => h.Category)
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .AsQueryable();

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

            query = model.Sorting switch
            {
                HouseSorting.PriceAscending => query.OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => query.OrderBy(h => h.RenterId != null).ThenByDescending(h => h.Id),
                HouseSorting.Newest => query.OrderByDescending(h => h.Id),
                _ => query.OrderByDescending(h => h.Id)
            };

            var totalCount = await query.CountAsync();

            var houses = await query
                .Skip((model.CurrentPage - 1) * AllHousesQueryModel.HousesPerPage)
                .Take(AllHousesQueryModel.HousesPerPage)
                .Select(h => new HouseViewModel
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
                })
                .ToListAsync();

            var categories = await context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();

            model.Houses = houses;
            model.TotalHousesCount = totalCount;
            model.Categories = categories;

            return Ok(model);
        }

        [Authorize]
        [HttpGet("Mine")]
        [Produces(typeof(IEnumerable<HouseViewModel>))]
        public async Task<IActionResult> Mine()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var houses = await context.Houses
                .AsNoTracking()
                .Include(h => h.Category)
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .Where(h => h.Agent.UserId == userId)
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseViewModel
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
                })
                .ToListAsync();

            return Ok(houses);
        }

        [HttpGet("{id}")]
        [Produces(typeof(HouseDetailsViewModel))]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await context.Houses
                .AsNoTracking()
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .Include(h => h.Category)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
            {
                return NotFound(new { message = "House not found" });
            }

            var model = new HouseDetailsViewModel
            {
                Id = house.Id,
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Description = house.Description,
                PricePerMonth = house.PricePerMonth,
                Category = house.Category?.Name ?? string.Empty,
                IsRented = house.RenterId != null,
                OwnerName = house.Agent.User.UserName ?? string.Empty,
                OwnerEmail = house.Agent.User.Email ?? string.Empty
            };

            return Ok(model);
        }

        [Authorize]
        [HttpPost]
        [Produces(typeof(HouseDetailModel))]
        public async Task<IActionResult> Create([FromBody] HouseDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid house data",
                    errors = ModelState
                });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var agent = await context.Agents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (agent == null)
            {
                return BadRequest(new { message = "Only agents can add houses." });
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
                AgentId = agent.Id
            };

            context.Houses.Add(newHouse);
            await context.SaveChangesAsync();

            return Created($"/api/House/{newHouse.Id}", new HouseDetailModel
            {
                Id = newHouse.Id,
                Address = newHouse.Address,
                ImageUrl = newHouse.ImageUrl,
                Title = newHouse.Title,
                Description = newHouse.Description,
                PricePerMonth = newHouse.PricePerMonth,
                Category = model.Category
            });
        }

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
