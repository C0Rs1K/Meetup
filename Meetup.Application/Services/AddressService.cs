using AutoMapper;
using Meetup.Application.Models.Address;
using Meetup.Application.Services.Intarfaces;
using Meetup.Domain.Models;
using Meetup.Infrastracture.Repositories.Interfaces;
using Meetup.Infrastracture.Shared.Exceptions;

namespace Meetup.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IQueryable<AddressModel> GetAll()
        {
            var addresses = _repository.GetAll();
            return _mapper.Map<IEnumerable<AddressModel>>(addresses).AsQueryable();
        }

        public async Task<AddressModel> GetByIdAsync(int id)
        {
            var address = await _repository.GetByIdAsync(id);

            if (address == null)
            {
                throw new NotFoundException("Address is not found");
            }

            return _mapper.Map<AddressModel>(address);
        }

        public async Task<AddressModel> InsertAsync(InsertAddressModel entity)
        {
            var address = _mapper.Map<AddressEntity>(entity);
            await _repository.InsertAsync(address);
            return _mapper.Map<AddressModel>(address);
        }

        public async Task RemoveAsync(int id)
        {
            var address = await _repository.GetByIdAsync(id);

            if (address == null)
            {
                throw new NotFoundException("Address is not found");
            }

            await _repository.RemoveAsync(address);
        }

        public async Task UpdateAsync(int id, UpdateAddressModel entity)
        {
            var address = await _repository.GetByIdAsync(id);

            if (address == null)
            {
                throw new NotFoundException("Address is not found");
            }

            await _repository.UpdateAsync(_mapper.Map<AddressEntity>(entity));
        }
    }
}
