namespace HouseRentingSystemApi.Models
{
    public class IndexViewModel
    {
        public int TotalHouses { get; set; }

        public int TotalRents { get; set; }

        public IEnumerable<HouseViewModel> Houses { get; set; } = new List<HouseViewModel>();
    }
}
