using Meetup.Domain.Models;
using Meetup.Infrastracture.DataBase;
using Meetup.Infrastracture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meetup.Infrastracture.Repositories
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly MeetupDbContext _context;
        private IQueryable<SpeakerEntity> Query
            => _context.Speakers.AsNoTracking();
        public SpeakerRepository(MeetupDbContext context)
        {
            _context = context;
        }

        public IQueryable<SpeakerEntity> GetAll()
        {
            return Query;
        }

        public async Task<SpeakerEntity> GetByIdAsync(int Id)
        {
            var speaker = await Query.Include(x => x.MeetupSpeakerEntities).ThenInclude(x => x.Meetup).FirstOrDefaultAsync(x => x.Id == Id);
            return speaker;
        }

        public async Task<EntityEntry<SpeakerEntity>> InsertAsync(SpeakerEntity entity)
        {
            var result = await _context.Speakers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<EntityEntry<SpeakerEntity>> RemoveAsync(SpeakerEntity entity)
        {
            var result = _context.Speakers.Remove(entity);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<EntityEntry<SpeakerEntity>> UpdateAsync(SpeakerEntity entity)
        {
            var result = _context.Speakers.Update(entity);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
