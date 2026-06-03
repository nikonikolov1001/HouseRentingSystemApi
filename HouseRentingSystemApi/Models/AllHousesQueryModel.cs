using HouseRentingSystemApi.Models.Enums;

namespace HouseRentingSystemApi.Models
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 6;

        public string? Category { get; set; }

        public string? SearchTerm { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public bool? IsRented { get; set; }

        public HouseSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = new List<string>();

        public IEnumerable<HouseViewModel> Houses { get; set; } = new List<HouseViewModel>();
    }
}
