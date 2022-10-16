using Meetup.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meetup.Infrastracture.Repositories.Interfaces
{
    public interface IMeetupRepository
    {
        IQueryable<MeetupEntity> GetAll();
        Task<MeetupEntity> GetByIdAsync(int Id);
        Task<EntityEntry<MeetupEntity>> InsertAsync(MeetupEntity entity);
        Task<EntityEntry<MeetupEntity>> UpdateAsync(MeetupEntity entity);
        Task<EntityEntry<MeetupEntity>> RemoveAsync(MeetupEntity entity);
        Task SaveChangesAsync();
    }
}
