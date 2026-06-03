using HouseRentingSystemApi.Models;

namespace HouseRentingSystemApi.Services.Houses
{
    public interface IHouseService
    {
        Task<IndexViewModel> GetIndexAsync();

        Task<AllHousesQueryModel> GetAllAsync(AllHousesQueryModel model);

        Task<IEnumerable<HouseViewModel>> GetMineAsync(string userId);

        Task<HouseDetailsViewModel?> GetByIdAsync(int id);

        Task<HouseDetailModel?> CreateAsync(HouseDetailModel model, string userId);
    }
}
