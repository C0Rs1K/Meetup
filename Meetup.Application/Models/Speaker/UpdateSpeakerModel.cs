using System.ComponentModel.DataAnnotations;

namespace Meetup.Application.Models.Speaker
{
    public class UpdateSpeakerModel
    {
        public UpdateSpeakerModel()
        {
            Meetups = Array.Empty<string>();
        }

        [Required]
        public string Name { get; set; }
        public string[] Meetups { get; set; }
    }
}
