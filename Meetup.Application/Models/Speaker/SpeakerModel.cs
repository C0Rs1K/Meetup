namespace Meetup.Application.Models.Speaker
{
    public class SpeakerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Meetups { get; set; }
    }
}
