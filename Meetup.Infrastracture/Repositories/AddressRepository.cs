using Meetup.Domain.Models;
using Meetup.Infrastracture.DataBase;
using Meetup.Infrastracture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meetup.Infrastracture.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly MeetupDbContext _context;

        public AddressRepository(MeetupDbContext context)
        {
            _context = context;
        }

        public IQueryable<AddressEntity> GetAll()
        {
            return _context.Addresses.AsNoTracking();
        }

        public async Task<AddressEntity> GetByIdAsync(int Id)
        {
            var address = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            return address;
        }

        public async Task<EntityEntry<AddressEntity>> InsertAsync(AddressEntity entity)
        {
            var result = await _context.Addresses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<EntityEntry<AddressEntity>> RemoveAsync(AddressEntity entity)
        {
            var result = _context.Addresses.Remove(entity);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<EntityEntry<AddressEntity>> UpdateAsync(AddressEntity entity)
        {
            var result = _context.Addresses.Update(entity);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
