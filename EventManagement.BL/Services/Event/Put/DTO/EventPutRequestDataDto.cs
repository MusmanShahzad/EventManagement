namespace EventManagement.BL.Services.Event.Put.DTO
{
    public class EventPutRequestDataDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
