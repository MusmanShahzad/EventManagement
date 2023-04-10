namespace EventManagement.BL.Services.User.Post.DTO
{
    public class UserRequestDataDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Guid> EventIds { get; set; }
        public List<string> Allergies { get; set; }
    }
}
