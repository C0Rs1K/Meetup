using AutoMapper;
using Meetup.Application.Models.Speaker;
using Meetup.Application.Services.Intarfaces;
using Meetup.Domain.Models;
using Meetup.Infrastracture.Repositories.Interfaces;
using Meetup.Infrastracture.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Application.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMeetupRepository _meetupRepository;
        private readonly IMapper _mapper;

        public SpeakerService(ISpeakerRepository repository, IMeetupRepository meetupRepository, IMapper mapper)
        {
            _speakerRepository = repository;
            _meetupRepository = meetupRepository;
            _mapper = mapper;
        }

        public IQueryable<SpeakerModel> GetAll()
        {
            var speakers = _speakerRepository.GetAll().Include(x => x.MeetupSpeakerEntities).ThenInclude(x => x.Meetup);
            return _mapper.Map<IEnumerable<SpeakerModel>>(speakers).AsQueryable();
        }

        public async Task<SpeakerModel> GetByIdAsync(int id)
        {
            var speaker = await _speakerRepository.GetByIdAsync(id);

            if (speaker == null)
            {
                throw new NotFoundException("Speaker is not found");
            }

            return _mapper.Map<SpeakerModel>(speaker);
        }

        public async Task<SpeakerModel> InsertAsync(InsertSpeakerModel insertModel)
        {
            var speaker = _mapper.Map<SpeakerEntity>(insertModel);
            await AddSpeakersMeetupsAsync(speaker, insertModel.Meetups);
            await _speakerRepository.InsertAsync(speaker);
            return _mapper.Map<SpeakerModel>(speaker);
        }

        public async Task RemoveAsync(int id)
        {
            var speaker = await _speakerRepository.GetByIdAsync(id);

            if (speaker == null)
            {
                throw new NotFoundException("Speaker is not found");
            }

            await _speakerRepository.RemoveAsync(speaker);
        }

        public async Task UpdateAsync(int id, UpdateSpeakerModel entity)
        {
            var speaker = await _speakerRepository.GetByIdAsync(id);

            if (speaker == null)
            {
                throw new NotFoundException("Meetup is not found");
            }

            var model = _mapper.Map<SpeakerEntity>(speaker);
            await AddSpeakersMeetupsAsync(model, entity.Meetups);
            await _speakerRepository.UpdateAsync(model);
        }

        private async Task AddSpeakersMeetupsAsync(SpeakerEntity speaker, string[] meetupNames)
        {
            speaker.MeetupSpeakerEntities.Clear();

            foreach (var meetupName in meetupNames)
            {
                var meetup = await _meetupRepository.GetAll().FirstOrDefaultAsync(x => x.Name == meetupName);

                if (meetup is not null)
                {
                    speaker.MeetupSpeakerEntities.Add(new MeetupSpeakerEntity
                    {
                        Speaker = speaker,
                        Meetup = meetup,
                        SpeakerId = speaker.Id,
                        MeetupId = meetup.Id
                    });

                    meetup.Id = 0;
                }
            }
        }
    }
}
