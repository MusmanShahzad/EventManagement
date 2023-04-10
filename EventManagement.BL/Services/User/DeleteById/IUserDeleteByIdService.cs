
namespace EventManagement.BL.Services.User.DeleteById
{
    using EventManagement.BL.CommonDto;
    public interface IUserDeleteByIdService
    {
        Task<ValidateDto> Validate(Guid Id);
        Task<Boolean> DeleteByIdAsync(Guid Id);

    }
}
