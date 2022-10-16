using AutoMapper;
using Meetup.Application.Models.Speaker;
using Meetup.Domain.Models;

namespace Meetup.Application.Profiles
{
    internal class SpeakerProfile : Profile
    {
        public SpeakerProfile()
        {
            CreateMap<SpeakerEntity, SpeakerModel>().ForMember(x => x.Meetups, opt => opt.MapFrom(x => x.MeetupSpeakerEntities.Select(x => x.Meetup.Name))).ReverseMap();
            CreateMap<InsertSpeakerModel, SpeakerEntity>().ForMember(x => x.MeetupSpeakerEntities, opt => opt.Ignore());
            CreateMap<UpdateSpeakerModel, SpeakerEntity>().ForMember(x => x.MeetupSpeakerEntities, opt => opt.Ignore());
        }
    }
}
