using Meetup.Domain.Models;
using Meetup.Infrastracture.DataBase;
using Meetup.Infrastracture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meetup.Infrastracture.Repositories
{
    public class MeetupRepository : IMeetupRepository
    {
        private readonly MeetupDbContext _context;
        private IQueryable<MeetupEntity> Query 
            => _context.Meetups.AsNoTracking();

        public MeetupRepository(MeetupDbContext context)
        {
            _context = context;
        }

        public IQueryable<MeetupEntity> GetAll()
        {
            return Query;
        }

        public async Task<MeetupEntity> GetByIdAsync(int Id)
        {
            var meetup = await Query.Include(x => x.Address).Include(x => x.MeetupSpeakerEntities).ThenInclude(x => x.Speaker).FirstOrDefaultAsync(x => x.Id == Id);
            return meetup;
        }

        public async Task<EntityEntry<MeetupEntity>> InsertAsync(MeetupEntity entity)
        {
            var result = await _context.Meetups.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<EntityEntry<MeetupEntity>> RemoveAsync(MeetupEntity entity)
        {
            var result = _context.Meetups.Remove(entity);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<EntityEntry<MeetupEntity>> UpdateAsync(MeetupEntity entity)
        {
            var result = _context.Meetups.Update(entity);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
