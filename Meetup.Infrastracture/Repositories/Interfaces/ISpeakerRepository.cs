using Meetup.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meetup.Infrastracture.Repositories.Interfaces
{
    public interface ISpeakerRepository
    {
        IQueryable<SpeakerEntity> GetAll();
        Task<SpeakerEntity> GetByIdAsync(int Id);
        Task<EntityEntry<SpeakerEntity>> InsertAsync(SpeakerEntity entity);
        Task<EntityEntry<SpeakerEntity>> UpdateAsync(SpeakerEntity entity);
        Task<EntityEntry<SpeakerEntity>> RemoveAsync(SpeakerEntity entity);
        Task SaveChangesAsync();
    }
}
