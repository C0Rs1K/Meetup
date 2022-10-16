using Meetup.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meetup.Infrastracture.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        IQueryable<AddressEntity> GetAll();
        Task<AddressEntity> GetByIdAsync(int Id);
        Task<EntityEntry<AddressEntity>> InsertAsync(AddressEntity entity);
        Task<EntityEntry<AddressEntity>> UpdateAsync(AddressEntity entity);
        Task<EntityEntry<AddressEntity>> RemoveAsync(AddressEntity entity);
        Task SaveChangesAsync();
    }
}
