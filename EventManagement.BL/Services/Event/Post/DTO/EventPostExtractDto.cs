namespace EventManagement.BL.Services.Event.Post.DTO
{
    using EventManagement.Models;
    public class EventPostExtractDto
    {
        public EventRequestDataDto RequestData { get; set; }
        public List<User> EventUsers { get; set; } 
    }
}
