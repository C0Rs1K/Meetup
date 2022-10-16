using Meetup.Application.Models.Address;

namespace Meetup.Application.Services.Intarfaces
{
    public interface IAddressService
    {
        public IQueryable<AddressModel> GetAll();
        public Task<AddressModel> GetByIdAsync(int id);
        public Task<AddressModel> InsertAsync(InsertAddressModel entity);
        public Task UpdateAsync(int id, UpdateAddressModel entity);
        public Task RemoveAsync(int id);
    }
}
