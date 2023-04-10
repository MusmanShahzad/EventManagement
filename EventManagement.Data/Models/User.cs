using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventManagement.Models
{
    [Index(nameof(User.Email), IsUnique = true)]
    public class User
    {
        [Required]
        public Guid id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; }

        public List<Allergy> Allergies { get; set; }
    }
}
