using EventManagement.Models;

namespace EventManagement.BL.Services.User.Put.DTO
{
    public class UserPutRequestDataDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Guid> EventIds { get; set; }
        public List<string> Allergies { get; set; }
    }
}
