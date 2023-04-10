namespace EventManagement.BL.Services.User.Post.DTO
{
    using EventManagement.Models;
    public class UserPostExtractDto
    {
        public UserRequestDataDto RequestData { get; set; }
        public List<Event> UsersEvents { get; set; }
        public List<Allergy> UserAllergies { get; set; }
    }
}
