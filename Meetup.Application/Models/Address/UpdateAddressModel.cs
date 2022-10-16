using System.ComponentModel.DataAnnotations;

namespace Meetup.Application.Models.Address
{
    public class UpdateAddressModel
    {
        [Required]
        [MaxLength(30)]
        public string Country { get; set; }
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Required]
        [MaxLength(15)]
        public string Street { get; set; }
        [Required]
        [Range(1, 100)]
        public int Building { get; set; }
    }
}
