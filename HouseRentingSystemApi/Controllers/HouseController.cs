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
    [Route("api/[controller]")]
    public class HouseController : ControllerBase
    {
        private AppDbContext context;

        public HouseController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("Index")]
        [Produces(typeof(IndexViewModel))]
        public async Task<IActionResult> GetIndex()
        {
            var totalHouses = await context.Houses.CountAsync();
            var totalRents = 0; // Will implement when Rent entity is added

            var houses = await context.Houses
                .AsNoTracking()
                .OrderByDescending(h => h.Id)
                .Take(3)
                .Select(h => new HouseViewModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    Description = h.Description,
                    PricePerMonth = h.PricePerMonth,
                    Category = h.Category.Name,
                    IsRented = false, // Will implement when Rent entity is added
                    OwnerName = h.Owner != null ? h.Owner.UserName ?? string.Empty : string.Empty
                })
                .ToListAsync();

            var model = new IndexViewModel
            {
                TotalHouses = totalHouses,
                TotalRents = totalRents,
                Houses = houses
            };

            return Ok(model);
        }

        [HttpGet("All")]
        [Produces(typeof(AllHousesQueryModel))]
        public async Task<IActionResult> GetAll([FromQuery] AllHousesQueryModel model)
        {
            var query = context.Houses.AsQueryable();

            // Filter by category
            if (!string.IsNullOrWhiteSpace(model.Category))
            {
                query = query.Where(h => h.Category.Name == model.Category);
            }

            // Search by term
            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                var searchTerm = model.SearchTerm.ToLower();
                query = query.Where(h =>
                    h.Title.ToLower().Contains(searchTerm) ||
                    h.Address.ToLower().Contains(searchTerm) ||
                    h.Description.ToLower().Contains(searchTerm)
                );
            }

            // Sorting
            switch (model.Sorting)
            {
                case HouseSorting.PriceAscending:
                    query = query.OrderBy(h => h.PricePerMonth);
                    break;
                case HouseSorting.NotRentedFirst:
                    query = query.OrderByDescending(h => h.Id); // Will add rent status when Rent entity is ready
                    break;
                case HouseSorting.Newest:
                default:
                    query = query.OrderByDescending(h => h.Id);
                    break;
            }

            // Get total count before paging
            var totalCount = await query.CountAsync();

            // Paging
            var houses = await query
                .Skip((model.CurrentPage - 1) * AllHousesQueryModel.HousesPerPage)
                .Take(AllHousesQueryModel.HousesPerPage)
                .Select(h => new HouseViewModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    Description = h.Description,
                    PricePerMonth = h.PricePerMonth,
                    Category = h.Category.Name,
                    IsRented = false, // Will implement when Rent entity is added
                    OwnerName = h.Owner != null ? h.Owner.UserName ?? string.Empty : string.Empty
                })
                .ToListAsync();

            // Get unique categories
            var categories = await context.Categories
                .AsNoTracking()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();

            model.Houses = houses;
            model.TotalHousesCount = totalCount;
            model.Categories = categories;

            return Ok(model);
        }

        [HttpGet("{id}")]
        [Produces(typeof(HouseViewModel))]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await context.Houses
                .Include(h => h.Owner)
                .Include(h => h.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
            {
                return NotFound(new { message = "House not found" });
            }

            var model = new HouseViewModel()
            {
                Id = house.Id,
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Description = house.Description,
                PricePerMonth = house.PricePerMonth,
                Category = house.Category?.Name ?? string.Empty,
                IsRented = false, // Will implement when Rent entity is added
                OwnerName = house.Owner?.UserName ?? string.Empty
            };

            return Ok(model);
        }

        [Authorize]
        [HttpPost]
        [Produces(typeof(HouseDetailModel))]
        public async Task<IActionResult> Create([FromBody] HouseDetailModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newHouse = new House()
            {
                Description = model.Description,
                PricePerMonth = model.PricePerMonth,
                Address = model.Address,
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                UserId = userId
            };

            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Name ==  model.Category
                .ToString());
            if(category == null)
            {
                var newCategory = new Category()
                {
                    Name = model.Category.ToString(),
                };
                context.Categories.Add(newCategory);
                await context.SaveChangesAsync();
                newHouse.CategoryId = newCategory.Id; 
                
            }
            else
            {
                newHouse.CategoryId = category.Id;
            }
            context.Houses.Add(newHouse);
            await context.SaveChangesAsync();
            return Created($"api/{newHouse.Id}", new HouseDetailModel()
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
    }
}