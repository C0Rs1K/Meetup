using Meetup.Domain.Models.Common;

namespace Meetup.Domain.Models
{
    public class AddressEntity : BaseEntity
    {
        public AddressEntity()
        {
            Meetups = new List<MeetupEntity>();
        }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }

        public virtual IList<MeetupEntity> Meetups { get; set; }
    }
}
