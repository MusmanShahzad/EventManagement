namespace EventManagement.BL.Services.User.Get
{
    using EventManagement.BL.Services.User.Get.DTO;
    public interface IUserGetService
    {
        Task<List<UserDto>> GetAsync();

    }
}
