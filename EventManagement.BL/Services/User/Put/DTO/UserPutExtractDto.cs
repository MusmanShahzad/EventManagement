namespace EventManagement.BL.Services.User.Put.DTO
{
    using EventManagement.Models;
    public class UserPutExtractDto
    {
        public List<Event> SelectedEvents { get; set; }
        public UserPutRequestDataDto RequestData { get; set; }
        public User ExistingUser { get; set; }
    }
}
