using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.Agent;

namespace HouseRentingSystemApi.Data.Entities
{
    public class Agent
    {
        public Guid Id { get; init; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public ICollection<House> ManagedHouses { get; set; } = new List<House>();
    }
}
