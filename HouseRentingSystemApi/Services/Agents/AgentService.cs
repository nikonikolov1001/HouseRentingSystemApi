using HouseRentingSystemApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemApi.Services.Agents
{
    public class AgentService : IAgentService
    {
        private readonly AppDbContext context;

        public AgentService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid?> GetAgentIdByUserIdAsync(string userId)
            => await context.Agents
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .Select(a => (Guid?)a.Id)
                .FirstOrDefaultAsync();

        public async Task<bool> ExistsByUserIdAsync(string userId)
            => await context.Agents
                .AsNoTracking()
                .AnyAsync(a => a.UserId == userId);
    }
}
