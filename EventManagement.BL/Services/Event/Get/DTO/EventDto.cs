namespace EventManagement.BL.Services.Event.Get.DTO
{
    using EventManagement.Models;
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public List<User> Users { get; set; }

        public int TotalUsers { get; set; }

    }
}
