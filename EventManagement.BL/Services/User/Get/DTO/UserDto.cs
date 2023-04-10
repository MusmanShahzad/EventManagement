namespace EventManagement.BL.Services.User.Get.DTO
{
    using EventManagement.Models;
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public List<Event> Event { get; set; }

        public int TotalEvent { get; set; }

        public List<Allergy> Allergies { get; set; }

        public int TotalAllergies { get; set; }

    }
}
