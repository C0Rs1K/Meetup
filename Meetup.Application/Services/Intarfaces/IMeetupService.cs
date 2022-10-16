using Meetup.Application.Models.Meetup;

namespace Meetup.Application.Services.Intarfaces
{
    public interface IMeetupService
    {
        public IQueryable<MeetupModel> GetAll();
        public Task<MeetupModel> GetByIdAsync(int id);
        public Task<MeetupModel> InsertAsync(InsertMeetupModel entity);
        public Task UpdateAsync(int id, UpdateMeetupModel entity);
        public Task RemoveAsync(int id);
    }
}
