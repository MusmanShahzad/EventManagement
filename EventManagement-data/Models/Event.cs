using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
