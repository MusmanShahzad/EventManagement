using System.Text.Json.Serialization;

namespace EventManagement.Models
{
    public class Allergy
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
