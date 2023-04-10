namespace EventManagement.BL.Services.User.Post
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.Services.User.Post.DTO;
    using EventManagement.Models;
    public interface IUserPostService
    {
        Task<User> PostAsync(UserRequestDataDto requestData);

        Task<ValidateDto> Validate(UserRequestDataDto requestData);
    }
}
