using Meetup.Domain.Models.Common;

namespace Meetup.Domain.Models
{
    public class MeetupEntity : BaseEntity
    {
        public MeetupEntity()
        {
            MeetupSpeakerEntities = new List<MeetupSpeakerEntity>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int AddressId { get; set; }

        public AddressEntity Address { get; set; }
        public virtual IList<MeetupSpeakerEntity> MeetupSpeakerEntities { get; set; }
    }
}
