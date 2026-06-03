using HouseRentingSystemApi.Data.DataConstants;
using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HouseRentingSystemApi.Services.Houses;

namespace HouseRentingSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService houseService;

        public HouseController(IHouseService houseService)
        {
            this.houseService = houseService;
        }

        [HttpGet("Index")]
        [Produces(typeof(IndexViewModel))]
        public async Task<IActionResult> GetIndex()
        {
            return Ok(await houseService.GetIndexAsync());
        }

        [HttpGet("All")]
        [Produces(typeof(AllHousesQueryModel))]
        public async Task<IActionResult> GetAll([FromQuery] AllHousesQueryModel model)
        {
            return Ok(await houseService.GetAllAsync(model));
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

            return Ok(await houseService.GetMineAsync(userId));
        }

        [HttpGet("{id}")]
        [Produces(typeof(HouseDetailsViewModel))]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await houseService.GetByIdAsync(id);

            if (house == null)
            {
                return NotFound(new { message = "House not found" });
            }

            return Ok(house);
        }

        [Authorize(Roles = AppRoles.Agent)]
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

            var house = await houseService.CreateAsync(model, userId);
            if (house == null)
            {
                return BadRequest(new { message = "Only agents can add houses." });
            }

            return Created($"/api/House/{house.Id}", house);
        }
    }
}
