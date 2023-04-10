namespace EventManagement.BL.Services.Event.Put.DTO
{
    using EventManagement.Models;
    public class EventPutExtractDto
    {
        public List<User> SelectedUsers { get; set; }
        public EventPutRequestDataDto RequestData { get; set; }
        public Event ExistingEvent { get; set; } 
    }
}
