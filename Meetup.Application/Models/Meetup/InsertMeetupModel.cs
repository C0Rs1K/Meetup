using Meetup.Application.Models.Address;
using System.ComponentModel.DataAnnotations;

namespace Meetup.Application.Models.Meetup
{
    public class InsertMeetupModel
    {
        public InsertMeetupModel()
        {
            Speakers = Array.Empty<string>();
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public InsertAddressModel Address { get; set; }
        [Required]
        public string[] Speakers { get; set; }
    }
}
