using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventManagement.Models
{
    [Index(nameof(Event.Name), IsUnique = true)]
    public class Event
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime LastUpdatedOn { get; set;} = DateTime.Now;

        public List<User> Users { get; set; }

    }
}
