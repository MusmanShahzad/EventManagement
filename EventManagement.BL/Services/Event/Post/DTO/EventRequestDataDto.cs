namespace EventManagement.BL.Services.Event.Post.DTO
{
    public class EventRequestDataDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
