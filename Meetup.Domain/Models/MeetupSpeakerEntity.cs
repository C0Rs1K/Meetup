using Meetup.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.Domain.Models
{
    public class MeetupSpeakerEntity : BaseEntity
    {
        public int MeetupId { get; set; }
        public int SpeakerId { get; set; }

        public virtual MeetupEntity Meetup { get; set; }
        public virtual SpeakerEntity Speaker { get; set; }
    }
}
