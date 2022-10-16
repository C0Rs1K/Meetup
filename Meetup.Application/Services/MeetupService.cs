using AutoMapper;
using Meetup.Application.Models.Meetup;
using Meetup.Application.Services.Intarfaces;
using Meetup.Domain.Models;
using Meetup.Infrastracture.Repositories.Interfaces;
using Meetup.Infrastracture.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Application.Services
{
    public class MeetupService : IMeetupService
    {
        private readonly IMeetupRepository _meetupRepository;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMapper _mapper;

        public MeetupService(IMeetupRepository meetupRepository, ISpeakerRepository speakerRepository, IMapper mapper)
        {
            _meetupRepository = meetupRepository;
            _speakerRepository = speakerRepository;
            _mapper = mapper;
        }

        public IQueryable<MeetupModel> GetAll()
        {
            var meetups = _meetupRepository.GetAll().Include(x => x.Address).Include(x => x.MeetupSpeakerEntities).ThenInclude(x => x.Speaker);
            return _mapper.Map<IEnumerable<MeetupModel>>(meetups).AsQueryable();
        }

        public async Task<MeetupModel> GetByIdAsync(int id)
        {
            var meetup = await _meetupRepository.GetByIdAsync(id);

            if (meetup == null)
            {
                throw new NotFoundException("Meetup is not found");
            }

            return _mapper.Map<MeetupModel>(meetup);
        }

        public async Task<MeetupModel> InsertAsync(InsertMeetupModel entity)
        {
            var meetup = _mapper.Map<MeetupEntity>(entity);
            await AddMeetupSpeakersAsync(meetup, entity.Speakers);
            await _meetupRepository.InsertAsync(meetup);
            return _mapper.Map<MeetupModel>(meetup);
        }

        public async Task RemoveAsync(int id)
        {
            var meetup = await _meetupRepository.GetByIdAsync(id);

            if (meetup == null)
            {
                throw new NotFoundException("Meetup is not found");
            }

            await _meetupRepository.RemoveAsync(meetup);
        }

        public async Task UpdateAsync(int id, UpdateMeetupModel entity)
        {
            var meetup = await _meetupRepository.GetByIdAsync(id);

            if (meetup == null)
            {
                throw new NotFoundException("Meetup is not found");
            }

            var model = _mapper.Map<MeetupEntity>(meetup);
            await AddMeetupSpeakersAsync(model, entity.Speakers);
            await _meetupRepository.UpdateAsync(model);
        }

        private async Task AddMeetupSpeakersAsync(MeetupEntity meetup, string[] speakerNames)
        {
            meetup.MeetupSpeakerEntities.Clear();

            foreach (var speakerName in speakerNames)
            {
                var speaker = await _speakerRepository.GetAll().FirstOrDefaultAsync(x => x.Name == speakerName);
                if (speaker is null)
                {
                    throw new NotFoundException("Speaker is not found");
                }

                meetup.MeetupSpeakerEntities.Add(new MeetupSpeakerEntity
                {
                    Speaker = speaker,
                    SpeakerId = speaker.Id,
                    MeetupId = meetup.Id,
                    Meetup = meetup
                });

                speaker.Id = 0;
            }
        }
    }
}
