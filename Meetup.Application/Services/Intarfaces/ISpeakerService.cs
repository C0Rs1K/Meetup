using Meetup.Application.Models.Meetup;
using Meetup.Application.Models.Speaker;

namespace Meetup.Application.Services.Intarfaces
{
    public interface ISpeakerService
    {
        public IQueryable<SpeakerModel> GetAll();
        public Task<SpeakerModel> GetByIdAsync(int id);
        public Task<SpeakerModel> InsertAsync(InsertSpeakerModel entity);
        public Task UpdateAsync(int id, UpdateSpeakerModel entity);
        public Task RemoveAsync(int id);
    }
}
