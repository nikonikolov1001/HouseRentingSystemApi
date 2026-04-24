namespace HouseRentingSystemApi.Models
{
    public class HouseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal PricePerMonth { get; set; }

        public string Category { get; set; } = string.Empty;

        public bool IsRented { get; set; }

        public string OwnerName { get; set; } = string.Empty;
    }
}
