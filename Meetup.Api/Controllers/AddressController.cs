using Meetup.Application.Models.Address;
using Meetup.Application.Services.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AddressModel>> Get()
        {
            var addresses = _service.GetAll();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressModel>> Get(int id)
        {
            var address = await _service.GetByIdAsync(id);
            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateAddressModel address)
        {
            await _service.UpdateAsync(id, address);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<AddressModel>> Post(InsertAddressModel address)
        {
            var model = await _service.InsertAsync(address);
            return CreatedAtAction(nameof(Post), model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
