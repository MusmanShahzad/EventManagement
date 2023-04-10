
namespace EventManagement.BL.Services.User.Get
{
    using EventManagement.BL.Services.User.Get.DTO;
    public interface IUserGetByIdService
    {
        Task<UserDto?> GetByIdAsync(Guid Id);

    }
}
