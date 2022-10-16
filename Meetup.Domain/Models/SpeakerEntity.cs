using Meetup.Domain.Models.Common;

namespace Meetup.Domain.Models
{
    public class SpeakerEntity : BaseEntity
    {
        public SpeakerEntity()
        {
            MeetupSpeakerEntities = new List<MeetupSpeakerEntity>();
        }

        public string Name { get; set; }

        public virtual IList<MeetupSpeakerEntity> MeetupSpeakerEntities { get; set; }
    }
}
