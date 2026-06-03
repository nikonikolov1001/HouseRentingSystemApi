namespace HouseRentingSystemApi.Services.Agents
{
    public interface IAgentService
    {
        Task<Guid?> GetAgentIdByUserIdAsync(string userId);

        Task<bool> ExistsByUserIdAsync(string userId);
    }
}
