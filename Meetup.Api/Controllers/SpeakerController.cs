using Meetup.Application.Models.Speaker;
using Meetup.Application.Services.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerService _service;

        public SpeakerController(ISpeakerService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SpeakerModel>> Get()
        {
            var speakers = _service.GetAll();
            return Ok(speakers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpeakerModel>> Get(int id)
        {
            var speaker = await _service.GetByIdAsync(id);
            return Ok(speaker);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateSpeakerModel speaker)
        {
            await _service.UpdateAsync(id, speaker);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SpeakerModel>> Post(InsertSpeakerModel speaker)
        {
            var model = await _service.InsertAsync(speaker);
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
