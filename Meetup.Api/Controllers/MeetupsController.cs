using Meetup.Application.Models.Meetup;
using Meetup.Application.Services.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly IMeetupService _service;

        public MeetupsController(IMeetupService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MeetupModel>> Get()
        {
            var meetups = _service.GetAll();
            return Ok(meetups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetupModel>> Get(int id)
        {
            var meetup = await _service.GetByIdAsync(id);
            return Ok(meetup);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateMeetupModel meetup)
        {
            await _service.UpdateAsync(id, meetup);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MeetupModel>> Post(InsertMeetupModel meetup)
        {
            var model = await _service.InsertAsync(meetup);
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
