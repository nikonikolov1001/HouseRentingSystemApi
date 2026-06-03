using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.House;

namespace HouseRentingSystemApi.Data.Entities
{
    public class House
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;
        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerMonth { get; set; }

        public Category Category { get; set; } = null!;
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; } 

        public Guid AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;

        public string? RenterId { get; set; }

        [ForeignKey(nameof(RenterId))]
        public ApplicationUser? Renter { get; set; }

    }
}
