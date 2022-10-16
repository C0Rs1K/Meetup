using AutoMapper;
using Meetup.Application.Models.Address;
using Meetup.Application.Models.Meetup;
using Meetup.Domain.Models;

namespace Meetup.Application.Profiles
{
    public class MeetupProfile : Profile
    {
        public MeetupProfile()
        {
            CreateMap<MeetupEntity, MeetupModel>().ForMember(x => x.Speakers, opt => opt.MapFrom(x => x.MeetupSpeakerEntities.Select(x => x.Speaker.Name))).ReverseMap();
            CreateMap<InsertMeetupModel, MeetupEntity>().ForMember(x => x.MeetupSpeakerEntities, opt => opt.Ignore());
            CreateMap<UpdateMeetupModel, MeetupEntity>().ForMember(x => x.MeetupSpeakerEntities, opt => opt.Ignore());
        }
    }
}
