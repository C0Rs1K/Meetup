using System.ComponentModel.DataAnnotations;

namespace Meetup.Application.Models.Speaker
{
    public class InsertSpeakerModel
    {
        public InsertSpeakerModel()
        {
            Meetups = Array.Empty<string>();
        }

        [Required]
        public string Name { get; set; }
        public string[] Meetups { get; set; }
    }
}
