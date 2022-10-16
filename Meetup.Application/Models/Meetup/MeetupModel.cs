using Meetup.Application.Models.Address;

namespace Meetup.Application.Models.Meetup
{
    public class MeetupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public AddressModel Address { get; set; }
        public string[] Speakers { get; set; }
    }
}
