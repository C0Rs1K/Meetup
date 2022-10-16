using System.ComponentModel.DataAnnotations.Schema;

namespace Meetup.Domain.Models.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
